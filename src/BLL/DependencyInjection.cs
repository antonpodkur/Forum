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
            services.AddAutoMapper(typeof(PostProfile), typeof(UserProfile), typeof(CommentProfile));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICommentService, CommentService>();
        }
    }
}