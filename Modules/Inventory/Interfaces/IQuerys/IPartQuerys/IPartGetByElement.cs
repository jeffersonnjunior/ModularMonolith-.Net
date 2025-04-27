using Modules.Inventory.Dtos.PartDtos;

namespace Modules.Inventory.Interfaces.IQuerys.IPartQuerys;

public interface IPartGetByElement
{
    PartReadDto GetById(Guid id);
}
