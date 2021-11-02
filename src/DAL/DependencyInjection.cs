using DAL.Abstractions.Repositories;
using DAL.Abstractions.UnitOfWork;
using DAL.DataContext;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class DependencyInjection
    {
        public static void AddDal(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ForumContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}