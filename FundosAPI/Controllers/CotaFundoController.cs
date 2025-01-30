using FundosAPI.Application.DTOs;
using FundosAPI.Application.Interfaces;
using FundosAPI.Application.Services;
using FundosAPI.Controllers.Base;
using FundosAPI.CrossCutting.ExcelReader;
using Microsoft.AspNetCore.Mvc;

namespace FundosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CotaFundoController : CrudBaseController<CotaFundoResponseDto, CotaFundoCreateDto, CotaFundoUpdateDto>
    {
        private CotaFundoService _service;

        public CotaFundoController(CotaFundoService service, ExcelFileReader excelFileReader) : base(service, excelFileReader)
        {
            _service = service;
        }

        [HttpGet("fundo/{id}")]
        public async Task<IActionResult> GetCotasDosFundos(int id)
        {
            var result = await _service.GetCotasPorFundo(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("cotas/{dataInicio:datetime}/{dataFim:datetime}")]
        public async Task<IActionResult> GetCotasPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            var result = await _service.GetCotasPeriodo(dataInicio, dataFim);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("cotas/{dataInicio:datetime}/{dataFim:datetime}/{fundoId:int}")]
        public async Task<IActionResult> GetCotasPorPeriodo(DateTime dataInicio, DateTime dataFim, int fundoId)
        {
            var result = await _service.GetCotasPeriodo(dataInicio, dataFim, fundoId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("seriesCotas/{dataInicio:datetime}/{dataFim:datetime}")]
        public async Task<IActionResult> GetSeriesCotasPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            var result = await _service.GetSerieCotas(dataInicio, dataFim);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("seriesCotas/{dataInicio:datetime}/{dataFim:datetime}/{fundoId:int}")]
        public async Task<IActionResult> GetSeriesCotasPorPeriodo(DateTime dataInicio, DateTime dataFim, int fundoId)
        {
            var result = await _service.GetSerieCotas(dataInicio, dataFim, fundoId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
