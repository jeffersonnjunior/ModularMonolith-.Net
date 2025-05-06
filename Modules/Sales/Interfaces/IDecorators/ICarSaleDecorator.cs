using Modules.Sales.Dtos.CarSaleDtos;

namespace Modules.Sales.Interfaces.IDecorators;

public interface ICarSaleDecorator
{
    CarSaleReadDto GetById(Guid id);
    CarSaleReturnFilterDto GetFilter(CarSaleGetFilterDto filter);
    CarSaleReadDto Create(CarSaleCreateDto carSaleCreateDto);
    void Update(CarSaleUpdateDto carSaleUpdateDto);
    void Delete(Guid id);
}
