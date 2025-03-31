using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Modules.LogisticsDistributionModule.Profiles;

namespace Modules.LogisticsDistributionModule.DependencyInjection;

public static class AddAutoMapperDependencyInjection
{
    public static void AutoMapperDependencyInjection(this IServiceCollection services)
    {
        var mapperConfig = AutoMapperConfig.RegisterMappings();
        services.AddSingleton(mapperConfig);
        services.AddScoped<IMapper>(sp => new Mapper(mapperConfig, sp.GetService));
    }
}