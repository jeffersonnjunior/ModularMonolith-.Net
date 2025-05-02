using Common.IPersistence.IRepositories;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.ICommands.IPartCommands;
using Modules.Inventory.Interfaces.IFactories;

namespace Modules.Inventory.Commands.PartCommands;

public class PartDeleteCommand : IPartDeleteCommand
{
    private readonly IBaseRepository _repository;

    public PartDeleteCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public void Delete(Part partReadDto)
    {
        _repository.Delete(partReadDto);

        _repository.SaveChanges();
    }
}