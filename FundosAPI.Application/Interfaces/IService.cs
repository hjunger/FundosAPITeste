using System.ComponentModel.DataAnnotations;

namespace FundosAPI.Application.Interfaces
{
    public interface IService<TDtoResponse, TDtoCreate, TDtoUpdate> where TDtoCreate : IDto
    {
        Task<IEnumerable<TDtoResponse>> GetAll();
        Task<TDtoResponse?> GetById(int id);
        Task<List<ValidationResult>> Insert(TDtoCreate dto);
        Task<List<ValidationResult>> Update(TDtoUpdate dto);
        Task<bool> Remove(int id);

        Task<List<ValidationResult>> UploadFile(IEnumerable<TDtoUpdate> dtos);
    }
}
