using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Production.Entities;
using Modules.Production.Interfaces.IQuerys.IProductionOrderQuerys;

namespace Modules.Production.Querys.ProductionOrderQuerys;

public class ProductionOrderGetByElement : IProductionOrderGetByElement
{
    private readonly IBaseRepository _repository;
    private readonly NotificationContext _notificationContext;
    public ProductionOrderGetByElement(IBaseRepository repository, NotificationContext notificationContext)
    {
        _repository = repository;
        _notificationContext = notificationContext;
    }

    public ProductionOrder GetById(Guid id)
    {
        ProductionOrder productionOrder = _repository.Find<ProductionOrder>(id);

        if (productionOrder == null)
        {
            _notificationContext.AddNotification("A peça com o ID especificado não foi encontrada.");
            return null;
        }

        return productionOrder;
    }
}
