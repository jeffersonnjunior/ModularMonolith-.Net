using Modules.Sales.Dtos.CarSaleDtos;

namespace Modules.Sales.Interfaces.IQuerys.ICarSaleQuerys;

public interface ICarSaleGetFilter
{
    CarSaleReturnFilterDto GetFilter(CarSaleGetFilterDto carSaleGetFilterDto);
}
