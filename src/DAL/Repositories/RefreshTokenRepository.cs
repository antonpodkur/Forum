using System.Threading.Tasks;
using DAL.Abstractions.Repositories;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class RefreshTokenRepository: Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DbContext context) : base(context)  { }

        public async Task<RefreshToken> GetByIdAsync(string id)
        {
            var refreshToken = await Context.Set<RefreshToken>().FindAsync((id));
            
            return refreshToken;
        }

        public void Update(RefreshToken refreshToken)
        {
            Context.Set<RefreshToken>().Update(refreshToken);
        }
    }
}