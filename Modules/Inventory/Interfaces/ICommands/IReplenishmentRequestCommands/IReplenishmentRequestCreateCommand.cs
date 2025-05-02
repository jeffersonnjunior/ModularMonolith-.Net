using Modules.Inventory.Entities;

namespace Modules.Inventory.Interfaces.ICommands.IReplenishmentRequestCommands;

public interface IReplenishmentRequestCreateCommand
{
    ReplenishmentRequest Add(ReplenishmentRequest replenishmentRequest);
}
