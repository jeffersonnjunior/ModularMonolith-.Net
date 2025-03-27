using Microsoft.Extensions.DependencyInjection;
using Modules.LogisticsDistributionModule.Command;
using Modules.LogisticsDistributionModule.Interfaces;

namespace Modules.LogisticsDistributionModule.DependencyInjection;

public static class AddLogisticsDistributionDependencyInjection
{
    public static void LogisticsDistributionDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICreateDeliveryCommandHandler, CreateDeliveryCommandHandler>();
        services.AddScoped<ICreateDistributorCommandHandler, CreateDistributorCommandHandler>();
    }
}