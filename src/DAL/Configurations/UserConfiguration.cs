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

            builder.HasData(
                new User()
                {
                    Id = 2,
                    Nickname = "Johnie",
                    Email = "johndoe@gmail.com",
                    Password = "123456"
                },
                new User()
                {
                    Id = 1,
                    Nickname = "Janie",
                    Email = "janedoe@gmail.com",
                    Password = "123456"
                },
                new User()
                {
                    Id = 3,
                    Nickname = "My",
                    Email = "myemail@gmail.com",
                    Password = "1234567"
                }
            );
        }
    }
}