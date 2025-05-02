using Modules.Inventory.Entities;

namespace Modules.Inventory.Interfaces.ICommands.IReplenishmentRequestCommands;

public interface IReplenishmentRequestDeleteCommand
{
    void Delete(ReplenishmentRequest replenishmentRequest);
}
