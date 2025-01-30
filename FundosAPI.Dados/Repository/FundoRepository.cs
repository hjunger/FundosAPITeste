using FundosAPI.Dados.Contexto;
using FundosAPI.Dados.Repository.Interfaces;
using FundosAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FundosAPI.Dados.Repository
{
    public class FundoRepository : RepositoryBase<Fundo>, IFundoRepository
    {
        public FundoRepository(SistemaFundoContext context) : base(context)
        {
        }

        public override async Task<Fundo?> FindById(int id)
        {
            return await _context.Fundos.FirstOrDefaultAsync(i => i.FundoId == id);
        }
    }
}
