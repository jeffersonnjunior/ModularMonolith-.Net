using Microsoft.Extensions.DependencyInjection;

namespace Modules.Inventory.DependencyInjection;

public static class AddInventoryDependencyInjection
{
    public static void InventoryDependencyInjection(this IServiceCollection services)
    {
        services.InventoryCommandDependencyInjection();
    }
}
