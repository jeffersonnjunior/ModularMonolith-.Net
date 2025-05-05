using Microsoft.Extensions.DependencyInjection;
using Modules.Production.Commands.ProductionOrderCommands;
using Modules.Production.Commands.ProductionPartCommands;
using Modules.Production.Interfaces.ICommands.IProductionOrderCommands;
using Modules.Production.Interfaces.ICommands.IProductionPartCommands;

namespace Modules.Production.DependencyInjection;

public static class AddProductionCommandDependencyInjection
{
    public static void ProductionCommandDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IProductionOrderCreateCommand, ProductionOrderCreateCommand>();
        services.AddScoped<IProductionOrderDeleteCommand, ProductionOrderDeleteCommand>();
        services.AddScoped<IProductionOrderUpdateCommand, ProductionOrderUpdateCommand>();
        services.AddScoped<IProductionPartCreateCommand, ProductionPartCreateCommand>();
        services.AddScoped<IProductionPartDeleteCommand, ProductionPartDeleteCommand>();
        services.AddScoped<IProductionPartUpdateCommand, ProductionPartUpdateCommand>();
    }
}