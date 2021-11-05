using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Abstractions.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetByIdAsync(string id);

        void Update(Post post);
    }
}