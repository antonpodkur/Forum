using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataContext
{
    public class ForumContextFactory: IDesignTimeDbContextFactory<ForumContext>
    {
        public ForumContext CreateDbContext(string[] args)
        {
            AppConfiguration appConfiguration = new AppConfiguration();
            var opsBuilder = new DbContextOptionsBuilder<ForumContext>();
            opsBuilder.UseSqlServer(appConfiguration.SqlConnectionString);
            return new ForumContext(opsBuilder.Options);
        }
    }
}