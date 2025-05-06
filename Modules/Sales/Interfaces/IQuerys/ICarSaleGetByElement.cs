using Modules.Sales.Entities;

namespace Modules.Sales.Interfaces.IQuerys;

public interface ICarSaleGetByElement
{
    CarSale GetById(Guid id);
}