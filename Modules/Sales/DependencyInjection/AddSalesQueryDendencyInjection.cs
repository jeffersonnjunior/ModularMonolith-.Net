using Microsoft.Extensions.DependencyInjection;
using Modules.Sales.Interfaces.IQuerys.ICarSaleQuerys;
using Modules.Sales.Interfaces.IQuerys.ISaleDetailQuerys;
using Modules.Sales.Querys.CarSaleQuerys;
using Modules.Sales.Querys.CarSaleQueys;
using Modules.Sales.Querys.SaleDetailQuerys;

namespace Modules.Sales.DependencyInjection;

public static class AddSalesQueryDendencyInjection
{
    public static void SalesQueryDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICarSaleGetByElement, CarSaleGetByElement>();
        services.AddScoped<ICarSaleGetFilter, CarSaleGetFilter>();
        services.AddScoped<ISaleDetailGetByElement, SaleDetailGetByElement>();
        services.AddScoped<ISaleDetailGetFilter, SaleDetailGetFilter>();
    }
}