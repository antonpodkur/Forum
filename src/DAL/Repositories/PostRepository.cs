using System.Threading.Tasks;
using DAL.Abstractions.Repositories;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class PostRepository: Repository<Post>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context) {}
        public async Task<Post> GetByIdAsync(int id)
        {
            var post = await Context.Set<Post>().FindAsync(id);
            return post;
        }

        public void Update(Post post)
        {
            Context.Set<Post>().Update(post);
        }
    }
}