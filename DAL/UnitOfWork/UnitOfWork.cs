using DAL.DataContext;
using DAL.DataContext.Entities;
using DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ForumContext _context;

        public UnitOfWork(ForumContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        public IUserRepository Users { get; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}