using BLL.Abstractions;
using BLL.AutoMapperProfiles;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class DependencyInjection
    {
        public static void AddBll(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PostProfile), typeof(UserProfile));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
        }
    }
}