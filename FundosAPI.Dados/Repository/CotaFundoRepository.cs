using FundosAPI.Dados.Contexto;
using FundosAPI.Dados.Repository.Interfaces;
using FundosAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FundosAPI.Dados.Repository
{
    public class CotaFundoRepository : RepositoryBase<CotaFundo>, ICotaFundoRepository
    {
        public CotaFundoRepository(SistemaFundoContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<CotaFundo>> GetAll()
        {
            return await _context.Cotas.Include(c => c.Fundo).ToListAsync();
        }

        public override async Task<CotaFundo?> FindById(int id)
        {
            return await _context.Cotas.FirstOrDefaultAsync(i => i.CotaId == id);
        }

        public async Task<List<CotaFundo>> GetCotaFundosByFundoId(int fundoId)
        {
            return await _context.Cotas.Where(c => c.FundoId == fundoId).ToListAsync();
        }

        public async Task<List<CotaFundo>> GetCotasPorPeriodo(DateTime dataInicio, DateTime dataFim, int? fundoId = null)
        {
            var dataInicioDtOnly = DateOnly.FromDateTime(dataInicio);
            var dataFimDtOnly = DateOnly.FromDateTime(dataFim);

            return await _context.Cotas
                .Where(c => c.DataCota >= dataInicioDtOnly && c.DataCota <= dataFimDtOnly && (fundoId == null || c.FundoId == fundoId.Value))
                .Include(c => c.Fundo)
                .ToListAsync();
        }
    }
}
