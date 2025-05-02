using Microsoft.Extensions.DependencyInjection;
using Modules.Inventory.Interfaces.IQuerys.IPartQuerys;
using Modules.Inventory.Interfaces.IQuerys.IReplenishmentRequestQuerys;
using Modules.Inventory.Querys.PartQuerys;
using Modules.Inventory.Querys.ReplenishmentRequestQuerys;

namespace Modules.Inventory.DependencyInjection;

public static class AddInventoryQueryDependencyInjection
{
    public static void InventoryQueryDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPartGetByElement, PartGetByElement>();
        services.AddScoped<IPartGetFilter, PartGetFilter>();
        services.AddScoped<IReplenishmentRequestGetByElement, ReplenishmentRequestGetByElement>();
        services.AddScoped<IReplenishmentRequestGetFilter, ReplenishmentRequestGetFilter>();
    }
}