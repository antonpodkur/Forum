using System.Threading.Tasks;
using DAL.Abstractions.Repositories;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }
        public async Task<User> GetByIdAsync(string id)
        {
            var user = await Context.Set<User>().FindAsync(id);
            await Context.Entry(user).Collection(u => u.Posts).LoadAsync();
            return user;
        }

        public void Update(User user)
        {
            Context.Set<User>().Update(user);
        }
    }
}