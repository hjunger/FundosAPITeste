using FundosAPI.Application.Interfaces;
using FundosAPI.Application.Validators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FundosAPI.Application.DTOs
{
    public class FundoUpdateDto : IDto
    {
        [Required(ErrorMessage = "O Id do Fundo é necessário para atualização.")]
        public int FundoId { get; set; }
        [Required(ErrorMessage = "O nome do fundo precisa ser preenchido.")]
        public string FundoNome { get; set; }

        [ValidatorCnpj]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "O administrador é obritatório.")]
        public string Administrador { get; set; }

        [Required(ErrorMessage = "A data de início do fundo é obritatória.")]
        public DateOnly DataInicio { get; set; }
        public DateOnly? DataFim { get; set; }

        [JsonIgnore]
        public int Id { get => FundoId; set => FundoId = value; }
    }
}
