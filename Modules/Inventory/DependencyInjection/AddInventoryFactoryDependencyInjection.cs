using Microsoft.Extensions.DependencyInjection;
using Modules.Inventory.Factories;
using Modules.Inventory.Interfaces.IFactories;

namespace Modules.Inventory.DependencyInjection;

public static class AddInventoryFactoryDependencyInjection
{
    public static void InventoryFactoryDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPartFactory, PartFactory>();
     }
}
