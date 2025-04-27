using Modules.Inventory.Dtos.PartDtos;

namespace Modules.Inventory.Interfaces.ICommand.ICreate;

public interface IPartCreateCommand
{
    PartReadDto Add(PartCreateDto partCreateDto);
}
