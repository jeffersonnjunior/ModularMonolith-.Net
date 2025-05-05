using Microsoft.Extensions.DependencyInjection;
using Modules.Production.Commands.ProductionOrderCommands;
using Modules.Production.Interfaces.ICommands.IProductionOrderCommands;

namespace Modules.Production.DependencyInjection;

public static class AddProductionCommandDependencyInjection
{
    public static void ProductionCommandDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IProductionOrderCreateCommand, ProductionOrderCreateCommand>();
        services.AddScoped<IProductionOrderDeleteCommand, ProductionOrderDeleteCommand>();
        services.AddScoped<IProductionOrderUpdateCommand, ProductionOrderUpdateCommand>();
    }
}