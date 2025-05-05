using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Production.Entities;
using Modules.Production.Interfaces.IQuerys.IProductionPartQuerys;

namespace Modules.Production.Querys.ProductionPartQuerys;

public class ProductionPartGetByElement : IProductionPartGetByElement
{
    private readonly IBaseRepository _repository;
    private readonly NotificationContext _notificationContext;
    public ProductionPartGetByElement(IBaseRepository repository, NotificationContext notificationContext)
    {
        _repository = repository;
        _notificationContext = notificationContext;
    }

    public ProductionPart GetById(Guid id)
    {
        ProductionPart productionPart = _repository.Find<ProductionPart>(id);

        if (productionPart == null)
        {
            _notificationContext.AddNotification("A produção com o ID especificado não foi encontrada.");
            return null;
        }

        return productionPart;
    }
}