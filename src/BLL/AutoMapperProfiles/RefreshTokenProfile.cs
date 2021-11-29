using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.AutoMapperProfiles
{
    public class RefreshTokenProfile: Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<RefreshTokenDTO, RefreshToken>();
            CreateMap<RefreshToken, RefreshTokenDTO>();
        }
    }
}