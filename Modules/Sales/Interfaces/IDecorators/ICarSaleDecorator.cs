using Modules.Sales.Dtos.CarSaleDtos;
using Modules.Sales.Querys.CarSaleQuerys;

namespace Modules.Sales.Interfaces.IDecorators;

public interface ICarSaleDecorator
{
    CarSaleReadDto GetById(Guid id);
    CarSaleReturnFilterDto GetFilter(CarSaleGetFilterDto filter);
    CarSaleReadDto Create(CarSaleCreateDto carSaleCreateDto);
    void Update(CarSaleUpdateDto carSaleUpdateDto);
    void Delete(Guid id);
}
