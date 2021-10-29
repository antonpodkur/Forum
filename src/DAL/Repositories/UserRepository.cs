using DAL.Abstractions.Repositories;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }
    }
}