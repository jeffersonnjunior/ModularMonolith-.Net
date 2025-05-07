using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Sales.Entities;
using Modules.Sales.Interfaces.IQuerys.ISaleDetailQuerys;

namespace Modules.Sales.Querys.SaleDetailQuerys;

public class SaleDetailGetByElement : ISaleDetailGetByElement
{
    private readonly IBaseRepository _repository;
    private readonly NotificationContext _notificationContext;
    public SaleDetailGetByElement(IBaseRepository repository, NotificationContext notificationContext)
    {
        _repository = repository;
        _notificationContext = notificationContext;
    }

    public SaleDetail GetById(Guid id)
    {
        SaleDetail saleDetail = _repository.Find<SaleDetail>(id);

        if (saleDetail == null)
        {
            _notificationContext.AddNotification("O detalhe da venda do carro com o ID especificado não foi encontrada.");
            return null;
        }

        return saleDetail;
    }
}