using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstractions;
using BLL.DTOs;
using DAL.Abstractions.UnitOfWork;
using DAL.Entities;
using DAL.UnitOfWork;

namespace BLL.Services
{
    public class RefreshTokenService: IRefreshTokenService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        
        public async Task<RefreshTokenDTO> AddAsync(RefreshTokenDTO refreshTokenDto)
        {
            var refreshToken = _mapper.Map<RefreshToken>(refreshTokenDto);
            _unitOfWork.RefreshTokens.Add(refreshToken);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<RefreshTokenDTO>(refreshTokenDto);
        }

        public async Task<IEnumerable<RefreshTokenDTO>> GetAllAsync()
        {
            var refreshTokens = await _unitOfWork.RefreshTokens.GetAllAsync();
            var refreshTokenDtos = _mapper.Map<IEnumerable<RefreshToken>, IEnumerable<RefreshTokenDTO>>(refreshTokens);
            return refreshTokenDtos;
        }

        public async Task<RefreshTokenDTO> GetByIdAsync(string id)
        {
            var refreshToken = await _unitOfWork.RefreshTokens.GetByIdAsync(id);
            var refreshTokenDto = _mapper.Map<RefreshTokenDTO>(refreshToken);
            return refreshTokenDto;
        }

        public async Task UpdateAsync(RefreshTokenDTO refreshTokenDto)
        {
            var refreshToken = _mapper.Map<RefreshToken>(refreshTokenDto);
            _unitOfWork.RefreshTokens.Update(refreshToken);
            await _unitOfWork.CompleteAsync();
        }

        public async Task RemoveAsync(string id)
        {
            var refreshToken = await _unitOfWork.RefreshTokens.GetByIdAsync(id);
            _unitOfWork.RefreshTokens.Remove(refreshToken);
            await _unitOfWork.CompleteAsync();
        }

        public async Task RemoveAllTokensById(string id)
        {
            var tokensToRemove = _unitOfWork.RefreshTokens.Find(t => t.UserId == id).ToList();
            if (tokensToRemove.Count != 0)
            {
                foreach (var token in tokensToRemove)
                {
                    _unitOfWork.RefreshTokens.Remove(token);
                }

                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<RefreshTokenDTO> FirstOrDefaultAsync(string refreshToken)
        {
            var storedToken = await _unitOfWork.RefreshTokens.FirstOrDefaultAsync(refreshToken);
            return _mapper.Map<RefreshTokenDTO>(storedToken);
        }
    }
}