using AutoMapper;

namespace Modules.LogisticsDistributionModule.Profiles;

public class AutoMapperConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(cfg => { cfg.AddProfile(new ProfileConfigurationMap()); });
    }
}