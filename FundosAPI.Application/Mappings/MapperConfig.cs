using AutoMapper;
using FundosAPI.Application.Mappings.Profiles;

namespace FundosAPI.Application.Mappings
{
    public static class MapperConfig
    {
        public static MapperConfiguration InitializeAutomapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FundoProfile>();
                cfg.AddProfile<CotaFundoProfile>();
            });
        }
    }
}
