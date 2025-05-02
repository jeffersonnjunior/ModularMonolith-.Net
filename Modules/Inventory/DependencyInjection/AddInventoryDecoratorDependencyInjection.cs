using Microsoft.Extensions.DependencyInjection;
using Modules.Inventory.Decorators;
using Modules.Inventory.Interfaces.IDecorators;

namespace Modules.Inventory.DependencyInjection;

public static class AddInventoryDecoratorDependencyInjection
{
    public static void InventoryDecoratorDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPartDecorator, PartDecorator>();
        services.AddScoped<IReplenishmentRequestDecorator, ReplenishmentRequestDecorator>();
    }
}
