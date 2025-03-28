using Microsoft.Extensions.DependencyInjection;
using Modules.LogisticsDistributionModule.Command;
using Modules.LogisticsDistributionModule.Interfaces;

namespace Modules.LogisticsDistributionModule.DependencyInjection;

public static class AddCommandDependencyInjection
{
    public static void CommandDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICreateDeliveryCommand, CreateDeliveryCommand>();
        services.AddScoped<ICreateDistributorCommand, CreateDistributorCommand>();
    }
}