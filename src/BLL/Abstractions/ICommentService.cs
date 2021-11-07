using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Abstractions
{
    public interface ICommentService
    {
        Task<CommentDTO> AddAsync(CommentDTO commentDto);
        Task<IEnumerable<CommentDTO>> GetAllAsync();
        Task<CommentDTO> GetByIdAsync(string id);
        Task UpdateAsync(CommentDTO commentDto);
        Task RemoveAsync(string id);
    }
}