using Modules.Inventory.Dtos.PartDtos;

namespace Modules.Inventory.Interfaces.IQuerys.IPartQuerys;

public interface IPartGetByElement
{
    Part GetById(Guid id);
}
