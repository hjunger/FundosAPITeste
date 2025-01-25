using FundosAPI.Dados.Contexto;
using FundosAPI.Dados.Repository.Interfaces;
using FundosAPI.Entities;

namespace FundosAPI.Dados.Repository
{
    public class FundoRepository : RepositoryBase<Fundo>, IFundoRepository
    {
        public FundoRepository(SistemaFundoContext context) : base(context)
        {
        }

        public override Fundo? FindById(int id)
        {
            return _context.Fundos.FirstOrDefault(i => i.FundoId == id);
        }
    }
}
