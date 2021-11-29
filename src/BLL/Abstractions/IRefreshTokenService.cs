using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.Abstractions
{
    public interface IRefreshTokenService
    {
        Task<RefreshTokenDTO> AddAsync(RefreshTokenDTO refreshTokenDto);
        Task<IEnumerable<RefreshTokenDTO>> GetAllAsync();
        Task<RefreshTokenDTO> GetByIdAsync(string id);
        Task UpdateAsync(RefreshTokenDTO refreshTokenDto);
        Task RemoveAsync(string id);
        Task RemoveAllTokensById(string id);
        Task<RefreshTokenDTO> FirstOrDefaultAsync(string refreshToken);
        
        //TODO: finish IRefreshTokenService, add method that corresponds to their usage in AuthController
    }
}