using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Auth
{
    public class AuthForumContext: IdentityDbContext<IdentityUser>
    {
        public AuthForumContext(DbContextOptions<AuthForumContext> options) : base(options)
        {
        }
        
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}