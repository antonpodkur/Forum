using System;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Abstractions.Repositories
{
    public interface ICommentRepository: IRepository<Comment>
    {
        Task<Comment> GetByIdAsync(string id);

        void Update(Comment comment);
    }
}