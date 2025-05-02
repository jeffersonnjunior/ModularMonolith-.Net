using Modules.Inventory.Entities;

namespace Modules.Inventory.Interfaces.ICommands.IReplenishmentRequestCommands;

public interface IReplenishmentRequestUpdateCommand
{
    void Update(ReplenishmentRequest replenishmentRequest);
}
