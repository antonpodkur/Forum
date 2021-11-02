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

            
            /*
            builder.Property(p => p.User).IsRequired();
            
            builder.Property(p => p.UserId).IsRequired();
            */
            
            
            builder.HasData(
                new Post()
                {
                    Id = 1,
                    Title = "That is the first post!!!",
                    Body = "Its body of the first post, i am so happy",
                    UserId = 2
                },
                new Post()
                {
                    Id = 2,
                    Title = "That is the second post!!!",
                    Body = "Its body of the second post, i am so happy",
                    UserId = 1
                },
                new Post()
                {
                    Id = 3,
                    Title = "That is the third post!!!",
                    Body = "Its body of the third post, from second person",
                    UserId = 1
                },
                new Post()
                {
                    Id = 4,
                    Title = "That is the fourth post!!!",
                    Body = "Its body of the fourth post, i am so happy",
                    UserId = 3
                }
            );
        }
    }
}