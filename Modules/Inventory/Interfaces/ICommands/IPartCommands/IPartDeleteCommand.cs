using Modules.Inventory.Dtos.PartDtos;

namespace Modules.Inventory.Interfaces.ICommands.IPartCommands;

public interface IPartDeleteCommand
{
    void Delete(Part partReadDto);
}
