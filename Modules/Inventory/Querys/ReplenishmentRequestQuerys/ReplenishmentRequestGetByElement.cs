using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Inventory.Entities;
using Modules.Inventory.Interfaces.IQuerys.IReplenishmentRequestQuerys;

namespace Modules.Inventory.Querys.ReplenishmentRequestQuerys;

public class ReplenishmentRequestGetByElement : IReplenishmentRequestGetByElement
{
    private readonly IBaseRepository _repository;
    private readonly NotificationContext _notificationContext;

    public ReplenishmentRequestGetByElement(
        IBaseRepository repository,
        NotificationContext notificationContext)
    {
        _repository = repository;
        _notificationContext = notificationContext;
    }

    public ReplenishmentRequest GetById(Guid id)
    {
        ReplenishmentRequest request = _repository.Find<ReplenishmentRequest>(id);

        if (request == null)
        {
            _notificationContext.AddNotification("A solicitação de reposição com o ID especificado não foi encontrada.");
            return null;
        }

        return request;
    }
}