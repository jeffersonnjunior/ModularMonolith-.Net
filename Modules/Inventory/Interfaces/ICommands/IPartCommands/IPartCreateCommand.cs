using Modules.Inventory.Dtos.PartDtos;

namespace Modules.Inventory.Interfaces.ICommands.IPartCommands;

public interface IPartCreateCommand
{
    PartReadDto Add(PartCreateDto partCreateDto);
}
