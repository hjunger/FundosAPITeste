using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using FundosAPI.Application.Interfaces;
using ClosedXML.Excel;

namespace FundosAPI.CrossCutting.ExcelReader
{
    public class ExcelFileReader
    {
        public IEnumerable<IDto> ReadFiles(List<IFormFile> files, List<ValidationResult> results, Func<IDto> getNovoObjetoDto)
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
                                var dto = getNovoObjetoDto();
                                foreach (var cell in row.Cells())
                                {
                                    var propName = headers[cell.Address.ColumnNumber - 1];
                                    var property = dto.GetType().GetProperty(propName);
                                    if (property == null)
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
            if (cellValue == null || cellValue.Length == 0)
            {
                return null;
            }

            if (propertyType == typeof(string))
            {
                return cellValue;
            }

            var dataTime = DateTime.MinValue;
            if (propertyType == typeof(DateOnly) && DateTime.TryParse(cellValue, out dataTime))
            {
                return DateOnly.FromDateTime(dataTime);
            }

            if (propertyType == typeof(DateTime) && DateTime.TryParse(cellValue, out dataTime))
            {
                return dataTime;
            }

            return Convert.ChangeType(cellValue, propertyType);
        }
    }
}
