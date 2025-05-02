using Common.IPersistence.IRepositories;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.ICommands.IPartCommands;
using Modules.Inventory.Interfaces.IFactories;

namespace Modules.Inventory.Commands.PartCommands;

public class PartDeleteCommand : IPartDeleteCommand
{
    private readonly IBaseRepository _repository;
    private readonly IPartFactory _partFactory;

    public PartDeleteCommand(IBaseRepository repository, IPartFactory partFactory)
    {
        _repository = repository;
        _partFactory = partFactory;
    }

    public void Delete(Part partReadDto)
    {
        _repository.Delete(partReadDto);

        _repository.SaveChanges();
    }
}