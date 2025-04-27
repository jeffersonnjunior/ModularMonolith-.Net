using Modules.Inventory.Dtos.PartDtos;

namespace Modules.Inventory.Interfaces.IQuery;

public interface IPartGetByElement
{
    PartReadDto GetById(Guid id);
}
