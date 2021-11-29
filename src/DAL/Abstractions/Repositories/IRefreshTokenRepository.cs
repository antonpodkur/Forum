using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Abstractions.Repositories
{
    public interface IRefreshTokenRepository: IRepository<RefreshToken>
    {
        Task<RefreshToken> GetByIdAsync(string id);

        Task<RefreshToken> FirstOrDefaultAsync(string refreshToken);
        void Update(RefreshToken post);
    }
}