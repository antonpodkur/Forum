using DAL.Configurations;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataContext
{
    public class ForumContext: DbContext
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

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
        }
    }
}