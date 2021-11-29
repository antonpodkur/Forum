using System;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            
            builder.HasMany(u => u.Posts).WithOne(p => p.User).HasForeignKey(p => p.UserId).HasPrincipalKey(u=> u.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Comments).WithOne(c => c.User).HasForeignKey(c => c.UserId).HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasMany<RefreshToken>(u => u.RefreshTokens)
                .WithOne(r => r.User)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}