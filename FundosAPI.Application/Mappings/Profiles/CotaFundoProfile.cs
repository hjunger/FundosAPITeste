using AutoMapper;
using FundosAPI.Application.DTOs;
using FundosAPI.Entities;

namespace FundosAPI.Application.Mappings.Profiles
{
    public class CotaFundoProfile : Profile
    {
        public CotaFundoProfile()
        {
            CreateMap<CotaFundo, CotaFundoResponseDto>()
                .ForMember(c => c.FundoNome, opt => opt.MapFrom(f => f.Fundo.FundoNome));
            CreateMap<CotaFundoCreateDto, CotaFundo>();
            CreateMap<CotaFundoUpdateDto, CotaFundo>();
            CreateMap<CotaFundoUpdateDto, CotaFundoCreateDto>();
        }
    }
}
