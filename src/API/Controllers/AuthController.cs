using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Auth;
using API.Auth.DTOs.Requests;
using API.Auth.DTOs.Responses;
using API.Configuration;
using AutoMapper;
using BLL.Abstractions;
using BLL.DTOs;
using BLL.Services;
using DAL.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DAL.Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ForumContext _context;
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IMapper _mapper;

        public AuthController(UserManager<User> userManager, 
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptionsMonitor<JwtConfiguration> optionsMonitor, 
            TokenValidationParameters tokenValidationParameters,
            ForumContext context,
            IUserService userService,
            IRefreshTokenService refreshTokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtConfiguration = optionsMonitor.CurrentValue;
            _tokenValidationParameters = tokenValidationParameters;
            _context = context;
            _userService = userService;
            _refreshTokenService = refreshTokenService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    return BadRequest( new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Email already in use"
                        },
                        Success = false
                    });
                }

                var newUser = new User() {Email = user.Email, UserName = user.Username};
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);

                if (isCreated.Succeeded)
                {
                    // admin section
                    if (newUser.Email == "admin@gmail.com")
                    {
                        await CreateRole(Roles.Admin);
                        await _userManager.AddToRoleAsync(newUser, Roles.Admin);
                    }
                    // end of admin section
                    
                    var userDto = new UserDTO() //creating and adding user to my db
                    {
                        Id = newUser.Id,
                        Username = user.Username,
                        Email = user.Email,
                    };

                    var jwtToken = await GenerateJwtToken(newUser);
                    return Ok(new {user = userDto, jwtToken});
                }
                else
                {
                    return BadRequest( new RegistrationResponse()
                    {
                        Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                        Success = false
                    });
                }

            }

            return BadRequest( new RegistrationResponse()
            {
                Errors = new List<string>()
                {
                    "Invalid payload"
                },
                Success = false
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser == null)
                {
                    return BadRequest( new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Invalid login request"
                        },
                        Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
                if (!isCorrect)
                {
                    return BadRequest( new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Invalid payload"
                        },
                        Success = false
                    });
                }

                var userDto = _mapper.Map<UserDTO>(existingUser);

                var jwtToken = await GenerateJwtToken(existingUser);

                return Ok(new {user = userDto, jwtToken});
            }
            
            return BadRequest( new RegistrationResponse()
            {
                Errors = new List<string>()
                {
                    "Invalid payload"
                },
                Success = false
            });
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDto tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await VerifyAndGenerateToken(tokenRequest);

                if (result == null || result.Success == false)
                {
                    return BadRequest(new { jwtToken = result });
                }
                
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                
                var tokenVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters,
                    out var validatedToken);

                var userId = tokenVerification.Claims
                    .FirstOrDefault(x => x.Type == "Id").Value;

                var userDto = await _userService.GetByIdAsync(userId);


                return Ok(new {user = userDto, jwtToken = result});
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>()
                {
                    "Invalid payload"
                },
                Success = false
            });
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            var currentUserId = HttpContext.User.FindFirstValue("Id");
            Console.WriteLine(currentUserId);
            Console.WriteLine("hello");
            var user = await _userManager.FindByIdAsync(currentUserId);

            await _refreshTokenService.RemoveAllTokensById(currentUserId);
            return Ok();
        }
        

        private async Task<AuthResult> GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Secret);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _jwtConfiguration.Audience,
                Issuer = _jwtConfiguration.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(10), // 5-10 mins 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) 
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshTokenDTO()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = RandomString(35) + Guid.NewGuid()
            };

            await _refreshTokenService.AddAsync(refreshToken);
            
            return new AuthResult()
            {
                Token = jwtToken,
                Success = true,
                RefreshToken = refreshToken.Token
            };
        }
        
        private async Task<AuthResult> VerifyAndGenerateToken(TokenRequestDto tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                // validation 1 - validate jwtToken format

                var tokenVerification2 = jwtTokenHandler.ValidateToken(tokenRequest.Token,
                    _tokenValidationParameters,
                    out var validatedToken2);

                // validation 2 - validate encryption alg
                if(validatedToken2 is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if(result == false) {
                        return null;
                    }
                }

                // validation 3 - validate expiryDate
                var utcExpiryDate = long.Parse(tokenVerification2.Claims
                    .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token has not expired yet"
                        }
                    };
                }
                
                // validation 4 - validate existance of the token
                var storedToken = await _refreshTokenService.FirstOrDefaultAsync(tokenRequest.RefreshToken);

                if (storedToken == null)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token does not exist"
                        }
                    };
                }
                
                // validation 5 - validate if used
                if (storedToken.IsUsed)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token has been used"
                        }
                    };
                }
                
                // validation 6 - validate if revoked
                if (storedToken.IsRevoked)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token has been revoked"
                        }
                    };
                }

                // validation 7 - validate the id
                var jti = tokenVerification2.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if (storedToken.JwtId != jti)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token doesn`t match"
                        }
                    };
                }
                
                // update current token 

                storedToken.IsUsed = true;

                await _refreshTokenService.UpdateAsync(storedToken);

                // generate a new token
                var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
                return await GenerateJwtToken(dbUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970,  1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(chars, length).Select(x => x[random.Next(x.Length)]).ToArray());
        }

        private async Task CreateRole(string name)
        {
            var doesRoleExist = await _roleManager.RoleExistsAsync(name);
            if (!doesRoleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole(name));
            }
        }
        
        
    }
    //TODO: create repository for refresh token / add it via di, add dtos in bll.
}