using FundosAPI.Entities;

namespace FundosAPI.Dados.Repository.Interfaces
{
    public interface ICotaFundoRepository : IRepository<CotaFundo>
    {
        Task<List<CotaFundo>> GetCotaFundosByFundoId(int fundoId);

        Task<List<CotaFundo>> GetCotasPorPeriodo(DateTime dataInicio, DateTime dataFim, int? fundoId = null);
    }
}
