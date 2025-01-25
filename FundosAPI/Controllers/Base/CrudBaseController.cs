using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using FundosAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FundosAPI.Controllers.Base
{
    public abstract class CrudBaseController<TDtoResponse, TDtoCreate, TDtoUpdate> : ControllerBase
        where TDtoCreate : IDto
        where TDtoUpdate : IDto
    {
        protected IService<TDtoResponse, TDtoCreate, TDtoUpdate> Service;

        protected CrudBaseController(IService<TDtoResponse, TDtoCreate, TDtoUpdate> service)
        {
            Service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = Service.GetAll();
            if(result == null)
            {
                return NotFound();
            }

            return Ok(Service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = Service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TDtoCreate item)
        {
            var results = Service.Insert(item);

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
        public IActionResult Update([FromBody] TDtoUpdate item)
        {
            var results = Service.Update(item);
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
        public IActionResult Delete(int id)
        {
            if (Service.Remove(id))
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
        public IActionResult UploadArquivo(List<IFormFile> files)
        {
            if(files == null || files.Count == 0)
            {
                return BadRequest("Nenhum arquivo enviado.");
            }

            var results = new List<ValidationResult>();
            var filesContents = ReadFiles(files, results);
            if (results.Count > 0)
            {
                return BadRequest(results);
            }

            if(filesContents == null)
            {
                return BadRequest("Não foi possível ler os arquivos.");
            }

            results = Service.UploadFile(filesContents.ToList());
            if (results.Count > 0)
            {
                return BadRequest(results);
            }

            return Ok();
        }

        protected virtual IEnumerable<IDto> ReadFiles(List<IFormFile> files, List<ValidationResult> results)
        {
            var result = new List<IDto>();
            try
            {

                foreach (var file in files)
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        stream.Position = 0;

                        using (var workbook = new XLWorkbook(stream))
                        {
                            var worksheet = workbook.Worksheets.First();
                            var rows = worksheet.RangeUsed().RowsUsed();

                            var headerRow = rows.First(); // Assumes the first row is the header row
                            var headers = headerRow.Cells().Select(c => c.Value.ToString().Trim().Replace(" ", "")).ToList();

                            foreach (var row in rows.Skip(1))
                            {
                                var dto = GetNovoObjetoDto();
                                foreach (var cell in row.Cells())
                                {
                                    var propName = headers[cell.Address.ColumnNumber - 1];
                                    var property = dto.GetType().GetProperty(propName);
                                    if(property == null)
                                    {
                                        results.Add(new ValidationResult("Cabeçalho inválido."));
                                        return new List<IDto>();
                                    }

                                    var cellValue = cell.Value.ToString();
                                    var propertyType = property.PropertyType;
                                    var data = DateOnly.MinValue;
                                    var val = GetValor(cellValue, propertyType);
                                    property.SetValue(dto, data != DateOnly.MinValue ? data : val, null);
                                }
                                result.Add(dto);
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                results.Add(new ValidationResult($"Erro inesperado: {ex.Message}"));
                return new List<IDto>();
            }
        }

        private object GetValor(string cellValue, Type propertyType)
        {
            if(cellValue == null || cellValue.Length == 0)
            {
                return null;
            }

            if(propertyType == typeof(string))
            {
                return cellValue;
            }

            var dataTime = DateTime.MinValue;
            if(propertyType == typeof(DateOnly) && DateTime.TryParse(cellValue, out dataTime))
            {
                return DateOnly.FromDateTime(dataTime);
            }

            if (propertyType == typeof(DateTime) && DateTime.TryParse(cellValue, out dataTime))
            {
                return dataTime;
            }

            return Convert.ChangeType(cellValue, propertyType);
        }

        protected abstract IDto GetNovoObjetoDto();
    }
}
