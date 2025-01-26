using FundosAPI.Application.Services;
using FundosAPI.CrossCutting.ExcelReader;

namespace FundosAPI.Configuration
{
    public static class ServicesConfiguration
    {
        public static WebApplicationBuilder AddServicesConfiguration(this WebApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.Services.AddScoped<FundoService>();
            builder.Services.AddScoped<CotaFundoService>();

            builder.Services.AddScoped<ExcelFileReader>();

            return builder;
        }
    }
}
