using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Sales.Entities;
using Modules.Sales.Interfaces.IQuerys;

namespace Modules.Sales.Querys.CarSaleQueys;

public class CarSaleGetByElement : ICarSaleGetByElement
{
    private readonly IBaseRepository _repository;
    private readonly NotificationContext _notificationContext;

    public CarSaleGetByElement(IBaseRepository repository, NotificationContext notificationContext)
    {
        _repository = repository;
        _notificationContext = notificationContext;
    }

    public CarSale GetById(Guid id)
    {
        CarSale carSale = _repository.Find<CarSale>(id);

        if (carSale == null)
        {
            _notificationContext.AddNotification("A venda do carro com o ID especificado não foi encontrada.");
            return null;
        }

        return carSale;
    }
}