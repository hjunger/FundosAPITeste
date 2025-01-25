using FundosAPI.Application.DTOs;
using FundosAPI.Application.Interfaces;
using FundosAPI.Application.Services;
using FundosAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace FundosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FundoController : CrudBaseController<FundoResponseDto, FundoCreateDto, FundoUpdateDto>
    {
        public FundoController(FundoService service) : base(service)
        {
        }

        protected override IDto GetNovoObjetoDto()
        {
            return new FundoUpdateDto();
        }
    }
}
