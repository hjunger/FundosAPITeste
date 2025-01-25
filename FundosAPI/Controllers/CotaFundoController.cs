using FundosAPI.Application.DTOs;
using FundosAPI.Application.Interfaces;
using FundosAPI.Application.Services;
using FundosAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace FundosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CotaFundoController : CrudBaseController<CotaFundoResponseDto, CotaFundoCreateDto, CotaFundoUpdateDto>
    {
        public CotaFundoController(CotaFundoService service) : base(service)
        {
        }

        protected override IDto GetNovoObjetoDto()
        {
            return new CotaFundoUpdateDto();
        }
    }
}
