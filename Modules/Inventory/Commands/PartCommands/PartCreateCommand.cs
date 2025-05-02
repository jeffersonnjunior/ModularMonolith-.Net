using Common.IPersistence.IRepositories;
using Modules.Inventory.Interfaces.ICommands.IPartCommands;

namespace Modules.Inventory.Commands.PartCommands;

public class PartCreateCommand : IPartCreateCommand
{
    private readonly IBaseRepository _repository;

    public PartCreateCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public Part Add(Part part)
    {
        part = _repository.Add(part);
        _repository.SaveChanges();

        return part;
    }
}