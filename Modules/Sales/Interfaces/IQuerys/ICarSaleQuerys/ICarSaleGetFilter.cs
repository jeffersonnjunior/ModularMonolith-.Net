using Modules.Sales.Dtos.CarSaleDtos;
using Modules.Sales.Querys.CarSaleQuerys;

namespace Modules.Sales.Interfaces.IQuerys.ICarSaleQuerys;

public interface ICarSaleGetFilter
{
    CarSaleReturnFilterDto GetFilter(CarSaleGetFilterDto carSaleGetFilterDto);
}