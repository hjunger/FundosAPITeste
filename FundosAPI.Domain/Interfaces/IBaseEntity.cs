namespace FundosAPI.Domain.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; }
        DateTime DataCriacao { get; set; }
        DateTime? DataAtualizacao { get; set; }
    }
}