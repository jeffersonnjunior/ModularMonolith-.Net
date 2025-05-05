using Microsoft.Extensions.DependencyInjection;
using Modules.Production.Decorators;
using Modules.Production.Interfaces.IDecorators;

namespace Modules.Production.DependencyInjection;

public static class AddProductionDecoratorDependencyInjection
{
    public static void ProductionDecoratorDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IProductionOrderDecorator, ProductionOrderDecorator>();
    }
}