namespace FundosAPI.Application.DTOs
{
    public class CotaFundoSerieResponseDto
    {
        public int FundoId { get; set; }
        public string FundoNome { get; set; } = "";

        public List<string> Meses { get; set; } = new List<string>();

        public List<double> Valores { get; set; } = new List<double>();

    }
}
