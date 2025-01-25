using AutoMapper;
using FundosAPI.Application.DTOs;
using FundosAPI.Entities;

namespace FundosAPI.Application.Mappings.Profiles
{
    public class FundoProfile : Profile
    {
        public FundoProfile()
        {
            CreateMap<Fundo, FundoResponseDto>()
                .ForMember(f => f.CotasDoFundo, opt => opt.MapFrom(c => c.CotasDoFundo));
            CreateMap<FundoCreateDto, Fundo>();
            CreateMap<FundoUpdateDto, Fundo>();
            CreateMap<FundoUpdateDto, FundoCreateDto>();
        }
    }
}
