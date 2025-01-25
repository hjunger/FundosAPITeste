using FundosAPI.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundosAPI.Entities
{
    public class Fundo : IBaseEntity
    {
        [Key]
        public required int FundoId { get; set; }
        public required string FundoNome { get; set; }

        public required string Cnpj { get; set; }
        public required string Administrador { get; set; }

        public required DateOnly DataInicio { get; set; }
        public DateOnly? DataFim { get; set; }

        public ICollection<CotaFundo> CotasDoFundo { get; set; } = new List<CotaFundo>();

        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; set; }

        [NotMapped]
        public int Id { get => FundoId; }
    }
}
