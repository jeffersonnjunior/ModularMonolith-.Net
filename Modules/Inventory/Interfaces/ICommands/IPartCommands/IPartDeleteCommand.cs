namespace Modules.Inventory.Interfaces.ICommands.IPartCommands;

public interface IPartDeleteCommand
{
    void Delete(Part partReadDto);
}
