using Modules.Inventory.Dtos.PartDtos;

namespace Modules.Inventory.Interfaces.IQuerys.IPartQuerys;

public interface IPartGetFilter
{
    PartReturnFilterDto GetFilter(PartGetFilterDto filter);
}
