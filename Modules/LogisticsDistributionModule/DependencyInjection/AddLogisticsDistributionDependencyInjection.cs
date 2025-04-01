using Microsoft.Extensions.DependencyInjection;
using Modules.LogisticsDistributionModule.Command;
using Modules.LogisticsDistributionModule.Interfaces;

namespace Modules.LogisticsDistributionModule.DependencyInjection;

public static class AddLogisticsDistributionDependencyInjection
{
    public static void DependencyInjectionLogisticsDistribution(this IServiceCollection services)
    {
        services.CommandDependencyInjection();
        services.QueryDependencyInjection();
        services.AutoMapperDependencyInjection();
    }
}