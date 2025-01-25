namespace FundosAPI.Dados.Repository.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? FindById(int id);
        int Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
