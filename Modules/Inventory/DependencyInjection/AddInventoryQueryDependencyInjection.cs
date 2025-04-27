using Microsoft.Extensions.DependencyInjection;
using Modules.Inventory.Interfaces.IQuery;
using Modules.Inventory.Query.PartQuery;

namespace Modules.Inventory.DependencyInjection;

public static class AddInventoryQueryDependencyInjection
{
    public static void InventoryQueryDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPartGetByElement, PartGetByElement>();
    }
}
