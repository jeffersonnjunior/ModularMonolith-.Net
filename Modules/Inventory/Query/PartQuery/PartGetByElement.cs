using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Entities;
using Modules.Inventory.Interfaces.IQuery;
using Modules.Inventory.Interfaces.IFactory;

namespace Modules.Inventory.Query.PartQuery;

public class PartGetByElement : IPartGetByElement
{
    private readonly IBaseRepository _repository;
    private readonly IPartFactory _partFactory;
    private readonly NotificationContext _notificationContext;

    public PartGetByElement(IBaseRepository repository, IPartFactory partFactory, NotificationContext notificationContext)
    {
        _repository = repository;
        _partFactory = partFactory;
        _notificationContext = notificationContext;
    }

    public PartReadDto GetById(Guid id)
    {
        Part part = _repository.Find<Part>(id);

        if (part == null)
        {
            _notificationContext.AddNotification("A peça com o ID especificado não foi encontrada.");
            return null;
        }

        return _partFactory.MapToPartReadDto(part);
    }
}
