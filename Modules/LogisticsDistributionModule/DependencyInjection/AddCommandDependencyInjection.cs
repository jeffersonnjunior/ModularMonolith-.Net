using Microsoft.Extensions.DependencyInjection;
using Modules.LogisticsDistributionModule.Command;
using Modules.LogisticsDistributionModule.Command.Delete;
using Modules.LogisticsDistributionModule.Command.Update;
using Modules.LogisticsDistributionModule.Interfaces;
using Modules.LogisticsDistributionModule.Interfaces.ICommand.IDelete;
using Modules.LogisticsDistributionModule.Interfaces.ICommand.IUpdate;

namespace Modules.LogisticsDistributionModule.DependencyInjection;

public static class AddCommandDependencyInjection
{
    public static void CommandDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICreateDeliveryCommand, CreateDeliveryCommand>();
        services.AddScoped<ICreateDistributorCommand, CreateDistributorCommand>();
        services.AddScoped<IDeleteDistributorCommand, DeleteDistributorCommand>();
        services.AddScoped<IUpdateDistributorCommand, UpdateDistributorCommand>();
    }
}