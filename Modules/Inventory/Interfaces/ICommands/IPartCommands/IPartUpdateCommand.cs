using Modules.Inventory.Dtos.PartDtos;

namespace Modules.Inventory.Interfaces.ICommands.IPartCommands;

public interface IPartUpdateCommand
{
    void Update(Part part);
}
