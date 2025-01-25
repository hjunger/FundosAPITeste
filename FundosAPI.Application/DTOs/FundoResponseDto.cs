using FundosAPI.Entities;

namespace FundosAPI.Application.DTOs
{
    public class FundoResponseDto
    {
        public required int FundoId { get; set; }
        public required string FundoNome { get; set; }
        public required string Cnpj { get; set; }
        public required string Administrador { get; set; }
        public DateOnly DataInicio { get; set; }
        public DateOnly? DataFim { get; set; }

        public ICollection<CotaFundo> CotasDoFundo { get; set; } = new List<CotaFundo>();
    }
}
