using System;
using System.Threading.Tasks;
using DAL.Abstractions.Repositories;
using DAL.DataContext;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(ForumContext context) : base(context) { }
        public async Task<User> GetByIdAsync(string id)
        {
            var user = await Context.Set<User>().FindAsync(id);
            if (user != null)
            {
                await Context.Entry(user).Collection(u => u.Posts).LoadAsync();
                await Context.Entry(user).Collection(u => u.Comments).LoadAsync();
            }
            return user;
        }

        public void Update(User user)
        {
            Context.Set<User>().Update(user);
        }
    }
}