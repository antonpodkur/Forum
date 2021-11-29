using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Abstractions.Repositories
{
    public interface IRefreshTokenRepository: IRepository<RefreshToken>
    {
        Task<RefreshToken> GetByIdAsync(string id);

        void Update(RefreshToken post);
    }
}