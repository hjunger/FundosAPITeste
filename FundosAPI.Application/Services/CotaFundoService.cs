using AutoMapper;
using FundosAPI.Application.DTOs;
using FundosAPI.Application.Interfaces;
using FundosAPI.Dados.Repository.Interfaces;
using FundosAPI.Dados.UnitOfWork;
using FundosAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace FundosAPI.Application.Services
{
    public class CotaFundoService : BaseService<CotaFundo, CotaFundoResponseDto, CotaFundoCreateDto, CotaFundoUpdateDto>, IService<CotaFundoResponseDto, CotaFundoCreateDto, CotaFundoUpdateDto>
    {
        public CotaFundoService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        protected override ICotaFundoRepository Repository => UnitOfWork.CotaFundoRepository;

        protected override bool ValidaDto(object dto, List<ValidationResult> results)
        {
            var isValid = base.ValidaDto(dto, results);
            if (!isValid)
            {
                return isValid;
            }

            int fundoId = 0;
            DateOnly? dataCota = null;
            if(dto is CotaFundoCreateDto)
            {
                var dtoCreate = (CotaFundoCreateDto)dto;
                fundoId = dtoCreate.FundoId;
                dataCota = dtoCreate.DataCota;
            }

            if(dto is CotaFundoUpdateDto)
            {
                var dtoUpdate = (CotaFundoUpdateDto)dto;
                fundoId = dtoUpdate.FundoId;
                dataCota = dtoUpdate.DataCota;
            }

            if (dataCota == null)
            {
                results.Add(new ValidationResult("A data da cota deve ser informada."));
                return false;
            }

            if (fundoId == 0)
            {
                results.Add(new ValidationResult("Não foi possível determinar o fundo."));
                return false;
            }

            var fundo = UnitOfWork.FundoRepository.FindById(fundoId);
            if (fundo == null)
            {
                results.Add(new ValidationResult("Não foi possível determinar o fundo."));
                return false;
            }

            if(dataCota < fundo.DataInicio)
            {
                results.Add(new ValidationResult($"A data da cota não pode ser anterior a {fundo.DataInicio:dd/MM/yyyy}."));
                return false;
            }

            if(fundo.DataFim.HasValue && dataCota > fundo.DataFim.Value)
            {
                results.Add(new ValidationResult($"A data da cota não pode ser depois de {fundo.DataFim.Value:dd/MM/yyyy}."));
                return false;
            }

            return results.Count == 0;
        }
    }
}
