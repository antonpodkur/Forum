using System.Threading.Tasks;
using DAL.Abstractions.Repositories;
using DAL.Abstractions.UnitOfWork;
using DAL.DataContext;
using DAL.Repositories;

namespace DAL.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ForumContext _context;

        public UnitOfWork(ForumContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Posts = new PostRepository(_context);
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        public IUserRepository Users { get; }
        public IPostRepository Posts { get; }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}