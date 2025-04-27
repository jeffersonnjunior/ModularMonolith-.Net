using Modules.Inventory.Dtos.PartDtos;

namespace Modules.Inventory.Interfaces.ICommand.IUpdate;

public interface IPartUpdateCommand
{
    void Update(PartUpdateDto partUpdateDto);
}
