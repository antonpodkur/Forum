using System;
using System.Threading.Tasks;
using DAL.Abstractions.Repositories;

namespace DAL.Abstractions.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }
        Task<int> CompleteAsync();
    }
}