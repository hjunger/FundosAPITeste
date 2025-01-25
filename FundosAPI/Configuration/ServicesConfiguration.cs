using FundosAPI.Application.Services;
using FundosAPI.Dados.Contexto;
using FundosAPI.Dados.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace FundosAPI.Configuration
{
    public static class ServicesConfiguration
    {
        public static WebApplicationBuilder AddServicesConfiguration(this WebApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.Services.AddScoped<FundoService>();
            builder.Services.AddScoped<CotaFundoService>();

            return builder;
        }
    }
}
