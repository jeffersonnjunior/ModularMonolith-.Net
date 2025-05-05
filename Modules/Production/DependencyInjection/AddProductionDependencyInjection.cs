using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Production.DependencyInjection;

public static class AddProductionDependencyInjection
{
    public static void ProductionDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.ProductionCommandDependencyInjection();
        services.ProductionDecoratorDependencyInjection();
        services.ProductionFactoryDependencyInjection();
        services.ProductionQueryDependencyInjection();
    }
}