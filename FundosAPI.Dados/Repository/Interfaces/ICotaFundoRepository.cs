using FundosAPI.Entities;

namespace FundosAPI.Dados.Repository.Interfaces
{
    public interface ICotaFundoRepository : IRepository<CotaFundo>
    {
        List<CotaFundo> GetCotaFundosByFundoId(int fundoId);
    }
}
