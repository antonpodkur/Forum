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
            
            builder.Property(u => u.Id).IsRequired();
            builder.HasIndex(u => u.Id).IsUnique();
            
            builder.Property(s => s.Nickname).IsRequired(true).HasMaxLength(50);
            builder.HasIndex(e => e.Nickname).IsUnique();
            
            builder.Property(s => s.Email).IsRequired(true).HasMaxLength(320);
            builder.HasIndex(e => e.Email).IsUnique();
            
            builder.Property(s => s.Password).IsRequired(true).HasMaxLength(255);
            
            builder.HasMany(x => x.Posts);
            builder.HasMany(x => x.Posts).WithOne(p => p.User).HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Comments).WithOne(c => c.User).HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}