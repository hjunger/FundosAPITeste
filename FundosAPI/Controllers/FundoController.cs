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
    public class FundoController : CrudBaseController<FundoResponseDto, FundoCreateDto, FundoUpdateDto>
    {
        public FundoController(FundoService service, ExcelFileReader excelFileReader) : base(service, excelFileReader)
        {
        }

        protected override IDto GetNovoObjetoDto()
        {
            return new FundoUpdateDto();
        }
    }
}
