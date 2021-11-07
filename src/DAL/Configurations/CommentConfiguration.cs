using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class CommentConfiguration: IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasIndex(c => c.Id).IsUnique();

            builder.Property(c => c.Text).IsRequired().HasMaxLength(10000);

            /*builder.HasOne(c => c.User).WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Post).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.Restrict);*/
        }
    }
}