using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.ICommand.ICreate;
using Modules.Inventory.Interfaces.IFactory;

namespace Modules.Inventory.Command.PartCommand;

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

    public PartReadDto Add(PartCreateDto partCreateDto)
    {
        PartReadDto partReadDto = new PartReadDto();

        Part part = _partFactory.MapToPart(partCreateDto);
        
        part = _repository.Add(part); 
        _repository.SaveChanges();

        partReadDto = _partFactory.MapToPartReadDto(part);

        return partReadDto;
    }
}