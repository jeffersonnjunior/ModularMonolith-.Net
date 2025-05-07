using Microsoft.Extensions.DependencyInjection;
using Modules.Sales.Factories;
using Modules.Sales.Interfaces.IFactories;

namespace Modules.Sales.DependencyInjection;

public static class AddSalesFactoryDependencyInjection
{
    public static void SalesFactoryDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICarSaleFactory, CarSaleFactory>();
        services.AddScoped<ISaleDetailFactory, SaleDetailFactory>();
    }
}