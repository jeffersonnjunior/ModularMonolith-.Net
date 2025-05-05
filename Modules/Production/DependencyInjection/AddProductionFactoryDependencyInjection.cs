using Microsoft.Extensions.DependencyInjection;
using Modules.Production.Factories;
using Modules.Production.Interfaces.IFactories;

namespace Modules.Production.DependencyInjection;

public static class AddProductionFactoryDependencyInjection
{
    public static void ProductionFactoryDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IProductionOrderFactory, ProductionOrderFactory>();
    }
}