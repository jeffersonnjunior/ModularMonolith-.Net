using Microsoft.Extensions.DependencyInjection;
using Modules.Sales.Decorators;
using Modules.Sales.Interfaces.IDecorators;

namespace Modules.Sales.DependencyInjection;

public static class AddSalesDecoratorDependencyInjection
{
    public static void SalesDecoratorDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICarSaleDecorator, CarSaleDecorator>();
        services.AddScoped<ISaleDetailDecorator, SaleDetailDecorator>();
    }
}