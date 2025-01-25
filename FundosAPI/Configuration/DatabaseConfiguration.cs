using FundosAPI.Dados.Contexto;
using FundosAPI.Dados.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace FundosAPI.Configuration
{
    public static class DatabaseConfiguration
    {
        public static WebApplicationBuilder AddDatabaseConfiguration(this WebApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SistemaFundoContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            return builder;
        }
    }
}
