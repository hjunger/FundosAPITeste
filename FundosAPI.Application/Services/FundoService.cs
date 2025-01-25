using AutoMapper;
using FundosAPI.Application.DTOs;
using FundosAPI.Application.Interfaces;
using FundosAPI.Dados.Repository.Interfaces;
using FundosAPI.Dados.UnitOfWork;
using FundosAPI.Entities;

namespace FundosAPI.Application.Services
{
    public class FundoService : BaseService<Fundo, FundoResponseDto, FundoCreateDto, FundoUpdateDto>, IService<FundoResponseDto, FundoCreateDto, FundoUpdateDto> 
    {
        public FundoService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        protected override IFundoRepository Repository => UnitOfWork.FundoRepository;
    }
}
