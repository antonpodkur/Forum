using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Abstractions.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> GetByIdAsync(string id);

        void Update(User user);
    }
}