using FundosAPI.Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FundosAPI.Application.DTOs
{
    public class CotaFundoCreateDto : IDto
    {
        [Range(0, 0, ErrorMessage = "Não é possível informar o Id na criação")]
        public int CotaId { get; set; }

        [Required(ErrorMessage = "A data da Cota é obrigatória.")]
        public DateOnly DataCota { get; set; }

        [Required(ErrorMessage = "O valor da Cota é obrigatório.")]
        public double ValorCota { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o fundo.")]
        public int FundoId { get; set; }

        [JsonIgnore]
        public int Id { get=>CotaId; set => CotaId = value; }
    }
}
