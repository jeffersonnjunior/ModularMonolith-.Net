using Common.IPersistence.IRepositories;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.ICommand.ICreate;
using Modules.Inventory.Interfaces.IFactory;

namespace Modules.Inventory.Command.PartCommand;

public class PartCreateCommand : IPartCreateCommand
{
    private readonly IBaseRepository _repository;
    private readonly IPartFactory _partFactory;

    public PartCreateCommand(IBaseRepository repository, IPartFactory partFactory)
    {
        _repository = repository;
        _partFactory = partFactory;
    }

    public void Add(PartCreateDto partCreateDto)
    {
        Part part = _partFactory.MapToPart(partCreateDto);

        _repository.Add(part);
        _repository.SaveChanges();
    }
}