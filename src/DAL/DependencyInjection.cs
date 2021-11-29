using DAL.Abstractions.Repositories;
using DAL.Abstractions.UnitOfWork;
using DAL.DataContext;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
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
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            
            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ForumContext>().AddDefaultTokenProviders();
        }
    }
}