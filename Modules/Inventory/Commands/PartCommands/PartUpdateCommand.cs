using Common.IPersistence.IRepositories;
using Modules.Inventory.Interfaces.ICommands.IPartCommands;

namespace Modules.Inventory.Commands.PartCommands;

public class PartUpdateCommand : IPartUpdateCommand
{
    private readonly IBaseRepository _repository;

    public PartUpdateCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public void Update(Part part)
    {
        _repository.Update(part);
        _repository.SaveChanges();
    }
}