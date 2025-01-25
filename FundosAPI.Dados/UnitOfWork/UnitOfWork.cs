using FundosAPI.Dados.Contexto;
using FundosAPI.Dados.Repository;
using FundosAPI.Dados.Repository.Interfaces;

namespace FundosAPI.Dados.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private SistemaFundoContext _context;
        public UnitOfWork(SistemaFundoContext context)
        {
            _context = context;
        }

        private IFundoRepository _fundoRepository;
        public IFundoRepository FundoRepository
        {
            get 
            { 
                if(_fundoRepository == null)
                {
                    _fundoRepository = new FundoRepository(_context);
                }

                return _fundoRepository;
            }
        }

        private ICotaFundoRepository _cotaFundoRepository;
        public ICotaFundoRepository CotaFundoRepository
        {
            get 
            { 
                if(_cotaFundoRepository == null)
                {
                    _cotaFundoRepository = new CotaFundoRepository(_context);
                }

                return _cotaFundoRepository;
            }
        }
        
        public SistemaFundoContext SistemaFundoContext => _context;

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
