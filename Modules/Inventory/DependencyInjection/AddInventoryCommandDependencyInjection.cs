using Microsoft.Extensions.DependencyInjection;
using Modules.Inventory.Command.Create;
using Modules.Inventory.Command.Delete;
using Modules.Inventory.Command.Update;
using Modules.Inventory.Interfaces.ICommand.ICreate;
using Modules.Inventory.Interfaces.ICommand.IDelete;
using Modules.Inventory.Interfaces.ICommand.IUpdate;

namespace Modules.Inventory.DependencyInjection;

public static class AddInventoryCommandDependencyInjection
{
    public static void InventoryCommandDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPartCreateCommand, PartCreateCommand>();
        services.AddScoped<IPartDeleteCommand, PartDeleteCommand>();
        services.AddScoped<IPartUpdateCommand, PartUpdateCommand>();
    }
}
