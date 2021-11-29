using DAL.Configurations;
using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataContext
{
    public class ForumContext: IdentityDbContext<User>
    {
        public class OptionsBuild
        {
            public OptionsBuild()
            {
                Settings = new AppConfiguration();
                OpsBuilder = new DbContextOptionsBuilder<ForumContext>();
                OpsBuilder.UseSqlServer(Settings.SqlConnectionString);
                DbOptions = OpsBuilder.Options;
            }
            
            
            public DbContextOptionsBuilder<ForumContext> OpsBuilder { get; set; }
            public DbContextOptions<ForumContext> DbOptions { get; set; }
            private AppConfiguration Settings { get; set; }
        }
        
        public ForumContext(DbContextOptions<ForumContext> options) : base(options){}
        
        public static OptionsBuild ops = new OptionsBuild();

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
        }
    }
}