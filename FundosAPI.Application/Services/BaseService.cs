using AutoMapper;
using FundosAPI.Application.Interfaces;
using FundosAPI.Dados.Repository.Interfaces;
using FundosAPI.Dados.UnitOfWork;
using FundosAPI.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace FundosAPI.Application.Services
{
    public abstract class BaseService<TEntity, TDtoResponse, TDtoCreate, TDtoUpdate> : IService<TDtoResponse, TDtoCreate, TDtoUpdate> 
        where TDtoCreate : IDto
        where TDtoUpdate : IDto
        where TEntity : IBaseEntity
    {
        protected readonly IMapper Mapper;
        protected readonly IUnitOfWork UnitOfWork;        

        protected BaseService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        protected abstract IRepository<TEntity> Repository { get; }

        public IEnumerable<TDtoResponse> GetAll()
        {
            return Mapper.Map<IEnumerable<TDtoResponse>>(Repository.GetAll());
        }

        public TDtoResponse? GetById(int id)
        {
            var entity = Repository.FindById(id);
            if (entity == null)
            {
                return default;
            }

            return Mapper.Map<TDtoResponse>(entity);
        }

        public List<ValidationResult> Insert(TDtoCreate dto)
        {
            var results = new List<ValidationResult>();
            try
            {
                var isValid = ValidaDto(dto, results);
                if (results == null || results.Count == 0)
                {
                    var entity = Mapper.Map<TEntity>(dto);
                    Repository.Insert(entity);
                    UnitOfWork.SaveChanges();
                    dto.Id = entity.Id;
                }
            }
            catch(Exception ex)
            {
                var validation = new ValidationResult($"Erro inesperado: {ex.Message}");
                results.Add(validation);
            }

            return results;
        }

        public bool Remove(int id)
        {
            try
            {
                Repository.Delete(id);
                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                // TODO: Criar tratamentod e Log para ex
                return false;
            }
        }

        public List<ValidationResult> Update(TDtoUpdate dto)
        {
            var results = new List<ValidationResult>();
            try
            {
                var isValid = ValidaDto(dto, results);
                if (results == null || results.Count == 0)
                {
                    var entity = Mapper.Map<TEntity>(dto);
                    Repository.Update(entity);
                    UnitOfWork.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var validation = new ValidationResult($"Erro inesperado: {ex.Message}");
                results.Add(validation);
            }

            return results;
        }

        public List<ValidationResult> UploadFile(List<IDto> dtos)
        {
            using (var dbContext = UnitOfWork.SistemaFundoContext)
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    var resultUpload = new List<ValidationResult>();
                    foreach (var dto in dtos)
                    {
                        if (dto.Id == 0)
                        {
                            var dtoCreate = Mapper.Map<TDtoCreate>(dto);
                            var resultInsert = Insert(dtoCreate);
                            if (resultInsert != null && resultInsert.Count > 0)
                            {
                                resultUpload.AddRange(resultInsert);
                            }
                        }
                        else
                        {
                            var dtoUpdate = (TDtoUpdate)dto;
                            var resultUpdate = Update(dtoUpdate);
                            if (resultUpdate != null && resultUpdate.Count > 0)
                            {
                                resultUpload.AddRange(resultUpdate);
                            }
                        }
                    }

                    if (resultUpload.Count > 0)
                    {
                        transaction.Rollback();
                    }
                    else
                    {
                        dbContext.SaveChanges();
                        transaction.Commit();
                    }

                    return resultUpload;
                }
            }
        }

        protected virtual bool ValidaDto(object dto, List<ValidationResult> results)
        {
            var validatorContext = new ValidationContext(dto);
            var isValid = Validator.TryValidateObject(dto, validatorContext, results);
            return isValid;
        }
    }
}
