using FundosAPI.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace FundosAPI.Entities
{
    public class CotaFundo : IBaseEntity
    {
        [Key]
        public required int CotaId { get; set; }
        public required DateOnly DataCota { get; set; }
        public required double ValorCota { get; set; }

        public required int FundoId { get; set; }
        public Fundo Fundo { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; set; }

        public int Id => CotaId;
    }
}
