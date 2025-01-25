using FundosAPI.Application.Mappings.Profiles;

namespace FundosAPI.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static WebApplicationBuilder AddAutoMapperConfiguration(this WebApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            var profiles = new[] { 
                typeof(FundoProfile), 
                typeof(CotaFundoProfile) 
            };
            builder.Services.AddAutoMapper(profiles);
            return builder;
        }
    }
}
