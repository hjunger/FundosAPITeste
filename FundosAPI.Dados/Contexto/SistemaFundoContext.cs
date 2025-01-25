using FundosAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FundosAPI.Dados.Contexto
{
    public class SistemaFundoContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;

        public SistemaFundoContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public DbSet<Fundo> Fundos { get; set; }
        public DbSet<CotaFundo> Cotas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
