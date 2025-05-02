using Modules.Inventory.Entities;

namespace Modules.Inventory.Interfaces.IQuerys.IReplenishmentRequestQuerys;

public interface IReplenishmentRequestGetByElement
{
    ReplenishmentRequest GetById(Guid id);
}
