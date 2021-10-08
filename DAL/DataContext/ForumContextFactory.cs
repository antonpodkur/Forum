using DAL.DataContext.DataContext;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataContext
{
    public class ForumContextFactory : IDesignTimeDbContextFactory<ForumContext>
    {
        public ForumContext CreateDbContext(string[] args)
        {
            AppConfiguration appConfig = new AppConfiguration();
            var opsBuilder = new DbContextOptionsBuilder<ForumContext>();
            opsBuilder.UseSqlServer(appConfig.SqlConnectionString);
            return new ForumContext(opsBuilder.Options);
        }
    }
}