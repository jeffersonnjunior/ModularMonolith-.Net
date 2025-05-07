using Modules.Sales.Entities;

namespace Modules.Sales.Interfaces.IQuerys.ICarSaleQuerys;

public interface ICarSaleGetByElement
{
    CarSale GetById(Guid id);
}