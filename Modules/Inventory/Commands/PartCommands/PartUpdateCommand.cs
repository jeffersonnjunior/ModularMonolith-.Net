using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.ICommands.IPartCommands;
using Modules.Inventory.Interfaces.IFactories;
using Modules.Inventory.Interfaces.IQuerys.IPartQuerys;

namespace Modules.Inventory.Commands.PartCommands;

public class PartUpdateCommand : IPartUpdateCommand
{
    private readonly IBaseRepository _repository;
    private readonly IPartFactory _partFactory;
    private readonly IPartGetByElement _partGetByElement;
    private readonly NotificationContext _notificationContext;

    public PartUpdateCommand(IBaseRepository repository, IPartFactory partFactory, IPartGetByElement partGetByElement, NotificationContext notificationContext)
    {
        _repository = repository;
        _partFactory = partFactory;
        _partGetByElement = partGetByElement;
        _notificationContext = notificationContext;
    }

    public void Update(PartUpdateDto partUpdateDto)
    {
        if (_partGetByElement.GetById(partUpdateDto.Id) is null) return;

        Part updatedPart = _partFactory.MapToPartFromUpdateDto(partUpdateDto);

        _repository.Update(updatedPart);
        _repository.SaveChanges();
    }
}