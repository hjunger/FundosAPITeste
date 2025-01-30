namespace FundosAPI.Dados.Repository.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> FindById(int id);
        Task<int> Insert(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
