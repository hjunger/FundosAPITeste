using FundosAPI.Dados.Contexto;
using FundosAPI.Dados.Repository.Interfaces;
using FundosAPI.Entities;

namespace FundosAPI.Dados.Repository
{
    public class CotaFundoRepository : RepositoryBase<CotaFundo>, ICotaFundoRepository
    {
        public CotaFundoRepository(SistemaFundoContext context) : base(context)
        {
        }

        public override CotaFundo? FindById(int id)
        {
            return _context.Cotas.FirstOrDefault(i => i.CotaId == id);
        }

        public List<CotaFundo> GetCotaFundosByFundoId(int fundoId)
        {
            return _context.Cotas.Where(c => c.FundoId == fundoId).ToList();
        }
    }
}
