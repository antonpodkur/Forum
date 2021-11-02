using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Abstractions
{
    public interface IPostService
    {
        Task<PostDTO> AddAsync(PostDTO postDto);
        Task<IEnumerable<PostDTO>> GetAllAsync();
        Task<PostDTO> GetByIdAsync(int id);
        Task UpdateAsync(PostDTO postDto);
        Task RemoveAsync(int id);
    }
}