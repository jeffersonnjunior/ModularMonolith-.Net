using Microsoft.Extensions.DependencyInjection;
using Modules.Production.Interfaces.IQuerys.IProductionOrderQuerys;
using Modules.Production.Querys.ProductionOrderQuerys;

namespace Modules.Production.DependencyInjection;

public static class AddProductionQueryDependencyInjection
{
    public static void ProductionQueryDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IProductionOrderGetByElement, ProductionOrderGetByElement>();
        services.AddScoped<IProductionOrderGetFilter, ProductionOrderGetFilter>();
    }
}