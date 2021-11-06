using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Auth
{
    public class AuthForumContext: IdentityDbContext<AuthUser>
    {
        public AuthForumContext(DbContextOptions<AuthForumContext> options) : base(options)
        {
        }
        
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AuthUser>()
                .HasMany<RefreshToken>(u => u.RefreshTokens)
                .WithOne(r => r.User)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(builder);
        }
    }
}