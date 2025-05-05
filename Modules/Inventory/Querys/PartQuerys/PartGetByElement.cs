using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Inventory.Interfaces.IQuerys.IPartQuerys;

namespace Modules.Inventory.Querys.PartQuerys;

public class PartGetByElement : IPartGetByElement
{
    private readonly IBaseRepository _repository;
    private readonly NotificationContext _notificationContext;
    public PartGetByElement(
        IBaseRepository repository,
        NotificationContext notificationContext
        )
    {
        _repository = repository;
    }

    public Part GetById(Guid id)
    {
        Part part = _repository.Find<Part>(id);

        if (part == null)
        {
            _notificationContext.AddNotification("A peça com o ID especificado não foi encontrada.");
            return null;
        }

        return part;
    }
}