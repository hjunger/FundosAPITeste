using FundosAPI.Dados.Contexto;
using FundosAPI.Dados.Repository.Interfaces;
using FundosAPI.Domain.Interfaces;

namespace FundosAPI.Dados.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected SistemaFundoContext _context;

        protected RepositoryBase(SistemaFundoContext context)
        {
            _context = context;
        }

        public abstract T? FindById(int id);

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public int Insert(T entity)
        {
            var entidadeTipada = (IBaseEntity)entity;
            entidadeTipada.DataCriacao = DateTime.Now;
            _context.Set<T>().Add(entity);
            return entidadeTipada.Id;
        }

        public void Update(T entity)
        {
            ((IBaseEntity)entity).DataAtualizacao = DateTime.Now;
            _context.Set<T>().Update(entity);
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            if (entity == null) return;
            _context.Set<T>().Remove(entity);
        }
    }
}
