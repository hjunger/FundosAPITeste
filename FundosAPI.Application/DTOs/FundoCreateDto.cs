using FundosAPI.Application.Interfaces;
using FundosAPI.Application.Validators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FundosAPI.Application.DTOs
{
    public class FundoCreateDto : IDto
    {
        [Range(0,0, ErrorMessage = "Não é possível informar o Id na criação")]
        public int FundoId { get; set; }

        [Required(ErrorMessage = "O nome do fundo precisa ser preenchido.")]
        public required string FundoNome { get; set; }

        [ValidatorCnpj]
        public required string Cnpj { get; set; }

        [Required(ErrorMessage = "O administrador é obritatório.")]
        public required string Administrador { get; set; }

        [Required(ErrorMessage = "A data de início do fundo é obritatória.")]
        public DateOnly DataInicio { get; set; }
        public DateOnly? DataFim { get; set; }

        [JsonIgnore]
        public int Id { get => FundoId; set => FundoId = value; }
    }
}
