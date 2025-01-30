using FundosAPI.Application.Interfaces;
using FundosAPI.CrossCutting.ExcelReader;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace FundosAPI.Controllers.Base
{
    public abstract class CrudBaseController<TDtoResponse, TDtoCreate, TDtoUpdate> : ControllerBase
        where TDtoCreate : IDto
        where TDtoUpdate : IDto
    {
        protected IService<TDtoResponse, TDtoCreate, TDtoUpdate> Service;
        private ExcelFileReader _excelFileReader;

        protected CrudBaseController(IService<TDtoResponse, TDtoCreate, TDtoUpdate> service, ExcelFileReader excelFileReader)
        {
            Service = service;
            _excelFileReader = excelFileReader;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Service.GetAll();
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await Service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TDtoCreate item)
        {
            var results = await Service.Insert(item);

            if (results == null || results.Count == 0)
            {
                return Ok(item);
            }
            else
            {
                return BadRequest(results);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TDtoUpdate item)
        {
            var results = await Service.Update(item);
            if (results == null || results.Count == 0)
            {
                return Ok(item);
            }
            else
            {
                return BadRequest(results);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await Service.Remove(id);
            if (isDeleted)
            {
                return Ok("Item excluído com sucesso.");
            }
            else
            {
                return BadRequest("Ocorreu um erro na exclusão do item.");
            }
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadArquivo(List<IFormFile> files)
        {
            if(files == null || files.Count == 0)
            {
                return BadRequest("Nenhum arquivo enviado.");
            }

            var results = new List<ValidationResult>();
            var filesContents = _excelFileReader.ReadFiles<TDtoUpdate>(files, results);
            if (results.Count > 0)
            {
                return BadRequest(results);
            }

            if(filesContents == null)
            {
                return BadRequest("Não foi possível ler os arquivos.");
            }

            results = await Service.UploadFile(filesContents.ToList());
            if (results.Count > 0)
            {
                return BadRequest(results);
            }

            return Ok();
        }        

        //protected virtual IEnumerable<IDto> ReadFiles(List<IFormFile> files, List<ValidationResult> results)
        //{
        //    var result = new List<IDto>();
        //    try
        //    {

        //        foreach (var file in files)
        //        {
        //            using (var stream = new MemoryStream())
        //            {
        //                file.CopyTo(stream);
        //                stream.Position = 0;

        //                using (var workbook = new XLWorkbook(stream))
        //                {
        //                    var worksheet = workbook.Worksheets.First();
        //                    var rows = worksheet.RangeUsed().RowsUsed();

        //                    var headerRow = rows.First(); // Assumes the first row is the header row
        //                    var headers = headerRow.Cells().Select(c => c.Value.ToString().Trim().Replace(" ", "")).ToList();

        //                    foreach (var row in rows.Skip(1))
        //                    {
        //                        var dto = GetNovoObjetoDto();
        //                        foreach (var cell in row.Cells())
        //                        {
        //                            var propName = headers[cell.Address.ColumnNumber - 1];
        //                            var property = dto.GetType().GetProperty(propName);
        //                            if(property == null)
        //                            {
        //                                results.Add(new ValidationResult("Cabeçalho inválido."));
        //                                return new List<IDto>();
        //                            }

        //                            var cellValue = cell.Value.ToString();
        //                            var propertyType = property.PropertyType;
        //                            var data = DateOnly.MinValue;
        //                            var val = GetValor(cellValue, propertyType);
        //                            property.SetValue(dto, data != DateOnly.MinValue ? data : val, null);
        //                        }
        //                        result.Add(dto);
        //                    }
        //                }
        //            }
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        results.Add(new ValidationResult($"Erro inesperado: {ex.Message}"));
        //        return new List<IDto>();
        //    }
        //}

        //private object GetValor(string cellValue, Type propertyType)
        //{
        //    if(cellValue == null || cellValue.Length == 0)
        //    {
        //        return null;
        //    }

        //    if(propertyType == typeof(string))
        //    {
        //        return cellValue;
        //    }

        //    var dataTime = DateTime.MinValue;
        //    if(propertyType == typeof(DateOnly) && DateTime.TryParse(cellValue, out dataTime))
        //    {
        //        return DateOnly.FromDateTime(dataTime);
        //    }

        //    if (propertyType == typeof(DateTime) && DateTime.TryParse(cellValue, out dataTime))
        //    {
        //        return dataTime;
        //    }

        //    return Convert.ChangeType(cellValue, propertyType);
        //}
    }
}
