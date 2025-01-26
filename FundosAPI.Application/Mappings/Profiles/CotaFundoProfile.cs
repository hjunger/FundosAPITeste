using AutoMapper;
using FundosAPI.Application.DTOs;
using FundosAPI.Entities;

namespace FundosAPI.Application.Mappings.Profiles
{
    public class CotaFundoProfile : Profile
    {
        public CotaFundoProfile()
        {
            CreateMap<CotaFundo, CotaFundoResponseDto>();
            CreateMap<CotaFundoCreateDto, CotaFundo>();
            CreateMap<CotaFundoUpdateDto, CotaFundo>();
            CreateMap<CotaFundoUpdateDto, CotaFundoCreateDto>();
        }
    }
}
