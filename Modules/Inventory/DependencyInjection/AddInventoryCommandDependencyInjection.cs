using Microsoft.Extensions.DependencyInjection;
using Modules.Inventory.Commands.PartCommands;
using Modules.Inventory.Commands.ReplenishmentRequestCommands;
using Modules.Inventory.Interfaces.ICommands.IPartCommands;
using Modules.Inventory.Interfaces.ICommands.IReplenishmentRequestCommands;

namespace Modules.Inventory.DependencyInjection;

public static class AddInventoryCommandDependencyInjection
{
    public static void InventoryCommandDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPartCreateCommand, PartCreateCommand>();
        services.AddScoped<IPartDeleteCommand, PartDeleteCommand>();
        services.AddScoped<IPartUpdateCommand, PartUpdateCommand>();
        services.AddScoped<IReplenishmentRequestCreateCommand, ReplenishmentRequestCreateCommand>();
        services.AddScoped<IReplenishmentRequestDeleteCommand, ReplenishmentRequestDeleteCommand>();
        services.AddScoped<IReplenishmentRequestUpdateCommand, ReplenishmentRequestUpdateCommand>();
    }
}
