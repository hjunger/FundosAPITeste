using FundosAPI.Dados.Contexto;
using FundosAPI.Dados.Repository.Interfaces;

namespace FundosAPI.Dados.UnitOfWork
{
    public interface IUnitOfWork
    {
        IFundoRepository FundoRepository { get; }
        ICotaFundoRepository CotaFundoRepository { get; }

        SistemaFundoContext SistemaFundoContext { get; }

        void SaveChanges();
    }
}
