using System.ComponentModel.DataAnnotations;

namespace FundosAPI.Application.Interfaces
{
    public interface IService<TDtoResponse, TDtoCreate, TDtoUpdate> where TDtoCreate : IDto
    {
        IEnumerable<TDtoResponse> GetAll();
        TDtoResponse? GetById(int id);

        List<ValidationResult> Insert(TDtoCreate dto);
        List<ValidationResult> Update(TDtoUpdate dto);
        bool Remove(int id);

        List<ValidationResult> UploadFile(List<IDto> dtos);
    }
}
