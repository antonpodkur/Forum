using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Auth
{
    public class AuthUser: IdentityUser
    {
        public AuthUser() { }
        
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}