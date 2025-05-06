using Microsoft.Extensions.DependencyInjection;
using Modules.Sales.Interfaces.IQuerys;
using Modules.Sales.Querys.CarSaleQuerys;
using Modules.Sales.Querys.CarSaleQueys;

namespace Modules.Sales.DependencyInjection;

public static class AddSalesQueryDendencyInjection
{
    public static void SalesQueryDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICarSaleGetByElement, CarSaleGetByElement>();
        services.AddScoped<ICarSaleGetFilter, CarSaleGetFilter>();
    }
}
