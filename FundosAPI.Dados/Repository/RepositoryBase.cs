using FundosAPI.Dados.Contexto;
using FundosAPI.Dados.Repository.Interfaces;
using FundosAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FundosAPI.Dados.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected SistemaFundoContext _context;

        protected RepositoryBase(SistemaFundoContext context)
        {
            _context = context;
        }

        public abstract Task<T?> FindById(int id);

        public async virtual Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<int> Insert(T entity)
        {
            var entidadeTipada = (IBaseEntity)entity;
            entidadeTipada.DataCriacao = DateTime.Now;
            await _context.Set<T>().AddAsync(entity);
            return entidadeTipada.Id;
        }

        public async Task Update(T entity)
        {
            if (entity == null) return;
            ((IBaseEntity)entity).DataAtualizacao = DateTime.Now;
            _context.Set<T>().Update(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await FindById(id);
            if (entity == null) return;
            _context.Set<T>().Remove(entity);
        }
    }
}
