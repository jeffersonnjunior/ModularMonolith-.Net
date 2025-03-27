using Microsoft.Extensions.DependencyInjection;

namespace Modules.LogisticsDistributionModule.DependencyInjection;

public static class AddCommandDependencyInjection
{
    public static void CommandDependencyInjection(this IServiceCollection services)
    {
        services.CommandDependencyInjection();
    }
}