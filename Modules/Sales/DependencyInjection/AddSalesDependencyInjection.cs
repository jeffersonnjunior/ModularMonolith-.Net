using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Sales.DependencyInjection;

public static class AddSalesDependencyInjection
{
    public static void SalesDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.SalesCommandDependencyInjection();
        services.SalesDecoratorDependencyInjection();
        services.SalesFactoryDependencyInjection();
        services.SalesQueryDependencyInjection();
    }
}