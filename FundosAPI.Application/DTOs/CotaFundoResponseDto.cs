using FundosAPI.Application.Interfaces;

namespace FundosAPI.Application.DTOs
{
    public class CotaFundoResponseDto
    {
        public int CotaId { get; set; }
        public DateOnly DataCota { get; set; }
        public double ValorCota { get; set; }

        public int FundoId { get; set; }
    }
}
