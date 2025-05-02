using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.ICommands.IPartCommands;
using Modules.Inventory.Interfaces.IFactories;

namespace Modules.Inventory.Commands.PartCommands;

public class PartCreateCommand : IPartCreateCommand
{
    private readonly IBaseRepository _repository;
    private readonly IPartFactory _partFactory;
    private readonly NotificationContext _notificationContext;

    public PartCreateCommand(IBaseRepository repository, IPartFactory partFactory, NotificationContext notificationContext)
    {
        _repository = repository;
        _partFactory = partFactory;
        _notificationContext = notificationContext;
    }

    public Part Add(Part part)
    {   
        part = _repository.Add(part); 
        _repository.SaveChanges();

        return part;
    }
}