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

        protected override IDto GetNovoObjetoDto()
        {
            return new CotaFundoUpdateDto();
        }

        [HttpGet("fundo/{id}")]
        public IActionResult GetCotasDosFundos(int id)
        {
            var result = _service.GetCotasPorFundo(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
