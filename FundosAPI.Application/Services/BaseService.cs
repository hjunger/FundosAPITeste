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

        public async Task<IEnumerable<TDtoResponse>> GetAll()
        {
            return Mapper.Map<IEnumerable<TDtoResponse>>(await Repository.GetAll());
        }

        public async Task<TDtoResponse?> GetById(int id)
        {
            var entity = await Repository.FindById(id);
            if (entity == null)
            {
                return default;
            }

            return Mapper.Map<TDtoResponse>(entity);
        }

        public async Task<List<ValidationResult>> Insert(TDtoCreate dto)
        {
            var results = new List<ValidationResult>();
            try
            {
                var isValid = ValidaDto(dto, results);
                if (results == null || results.Count == 0)
                {
                    var entity = Mapper.Map<TEntity>(dto);
                    await Repository.Insert(entity);
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

        public async Task<bool> Remove(int id)
        {
            try
            {
                await Repository.Delete(id);
                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                // TODO: Criar tratamentod e Log para ex
                return false;
            }
        }

        public async Task<List<ValidationResult>> Update(TDtoUpdate dto)
        {
            var results = new List<ValidationResult>();
            try
            {
                var isValid = await ValidaDto(dto, results);
                if (results == null || results.Count == 0)
                {
                    var entity = Mapper.Map<TEntity>(dto);
                    await Repository.Update(entity);
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

        public async Task<List<ValidationResult>> UploadFile(IEnumerable<TDtoUpdate> dtos)
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
                            var resultInsert = await Insert(dtoCreate);
                            if (resultInsert != null && resultInsert.Count > 0)
                            {
                                resultUpload.AddRange(resultInsert);
                            }
                        }
                        else
                        {
                            var dtoUpdate = dto;
                            var resultUpdate = await Update(dtoUpdate);
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

        protected virtual async Task<bool> ValidaDto(object dto, List<ValidationResult> results)
        {
            var validatorContext = new ValidationContext(dto);
            var isValid = Validator.TryValidateObject(dto, validatorContext, results);
            return isValid;
        }
    }
}
