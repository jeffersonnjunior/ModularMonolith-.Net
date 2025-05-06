using Modules.Sales.Dtos.CarSaleDtos;

namespace Modules.Sales.Interfaces.IQuerys;

public interface ICarSaleGetFilter
{
    CarSaleReturnFilterDto GetFilter(CarSaleGetFilterDto carSaleGetFilterDto);
}
