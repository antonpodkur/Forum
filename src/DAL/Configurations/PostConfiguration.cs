using System;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");

            builder.Property(p => p.Id).IsRequired();
            builder.HasIndex(p => p.Id).IsUnique();
            
            builder.Property(p => p.Title).IsRequired(true).HasMaxLength(300);

            builder.Property(p => p.Body).HasMaxLength(40000);

            builder.HasMany(c => c.Comments).WithOne(c => c.Post).HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}