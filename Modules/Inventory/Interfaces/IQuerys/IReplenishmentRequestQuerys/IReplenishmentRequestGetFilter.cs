using Modules.Inventory.Dtos.ReplenishmentRequestDtos;

namespace Modules.Inventory.Interfaces.IQuerys.IReplenishmentRequestQuerys;

public interface IReplenishmentRequestGetFilter
{
    ReplenishmentRequestReturnFilterDto GetFilter(ReplenishmentRequestGetFilterDto filter);
}
