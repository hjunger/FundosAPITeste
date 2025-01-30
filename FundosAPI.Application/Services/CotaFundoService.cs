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
        private readonly Dictionary<int, Fundo> _dictFundos = new Dictionary<int, Fundo>();

        public CotaFundoService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        protected override ICotaFundoRepository Repository => UnitOfWork.CotaFundoRepository;

        protected override async Task<bool> ValidaDto(object dto, List<ValidationResult> results)
        {
            _dictFundos.Clear();

            var isValid = await base.ValidaDto(dto, results);
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

            var fundo = await GetFundoAsync(fundoId);
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

        private async Task<Fundo?> GetFundoAsync(int fundoId)
        {
            if(_dictFundos.TryGetValue(fundoId, out var fundoRetorno))
            {
                return fundoRetorno;
            }

            var fundo = await UnitOfWork.FundoRepository.FindById(fundoId);
            if(fundo != null)
            {
                _dictFundos[fundoId] = fundo;
            }
            return fundo;
        }

        public async Task<List<CotaFundoResponseDto>> GetCotasPorFundo(int fundoId)
        {
            var repository = UnitOfWork.CotaFundoRepository;
            var cotasDoFundo = await repository.GetCotaFundosByFundoId(fundoId);
            return Mapper.Map<List<CotaFundoResponseDto>>(cotasDoFundo);
        }

        public async Task<List<CotaFundoResponseDto>> GetCotasPeriodo(DateTime dataInicio, DateTime dataFim, int? fundoId = null)
        {
            var reposity = UnitOfWork.CotaFundoRepository;
            var cotas = await reposity.GetCotasPorPeriodo(dataInicio, dataFim, fundoId);
            return Mapper.Map<List<CotaFundoResponseDto>>(cotas);
        }

        public async Task<List<CotaFundoSerieResponseDto>> GetSerieCotas(DateTime dataInicio, DateTime dataFim, int? fundoId = null)
        {
            var cotas = await GetCotasPeriodo(dataInicio, dataFim, fundoId);
            var cotasPorFundo = cotas.GroupBy(c => new {c.FundoId, c.FundoNome}).ToList();
            var result = new List<CotaFundoSerieResponseDto>();

            var meses = new List<DateOnly>();
            var mes = new DateOnly(dataInicio.Year, dataInicio.Month, 1);
            var mesFinal = new DateOnly(dataFim.Year, dataFim.Month, 1);
            while (mes <= mesFinal)
            {
                meses.Add(mes);
                mes = mes.AddMonths(1);
            }

            foreach (var cotaFundo in cotasPorFundo)
            {
                var item = new CotaFundoSerieResponseDto { FundoId = cotaFundo.Key.FundoId, FundoNome = cotaFundo.Key.FundoNome };
                var menorData = cotaFundo.Min(c => c.DataCota);
                var maiorData = cotaFundo.Max(c => c.DataCota);
                foreach(var m in meses)
                {
                    item.Meses.Add($"{m:MM/yyyy}");
                    var cotasDoMes = cotaFundo.Where(c => c.DataCota.Month == m.Month && c.DataCota.Year == m.Year).OrderBy(c => c.DataCota).ToList();
                    if (cotasDoMes.Any())
                    {
                        item.Valores.Add(cotasDoMes[0].ValorCota);
                        continue;
                    }

                    var primeiroDiaProxMes = maiorData.AddMonths(1);
                    if (primeiroDiaProxMes.Month == m.Month && primeiroDiaProxMes.Year == m.Year)
                    {
                        item.Valores.Add(cotaFundo.Last().ValorCota);
                        continue;
                    }

                    item.Valores.Add(0);
                }

                var ultimoMes = mesFinal.AddMonths(1);
                item.Meses.Add($"{ultimoMes:MM/yyyy}");
                if(maiorData.Year == mesFinal.Year && maiorData.Month == mesFinal.Month)
                {
                    item.Valores.Add(cotaFundo.FirstOrDefault(c => c.DataCota == maiorData)?.ValorCota ?? 0);
                }
                else
                {
                    item.Valores.Add(0);
                }
                
                result.Add(item);
            }

            return result;
        }
    }
}
