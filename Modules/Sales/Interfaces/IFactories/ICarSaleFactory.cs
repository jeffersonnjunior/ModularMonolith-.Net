using Modules.Sales.Dtos.CarSaleDtos;
using Modules.Sales.Entities;

namespace Modules.Sales.Interfaces.IFactories;

public interface ICarSaleFactory
{
    CarSale CreateCarSale();
    CarSale MapToCarSale(CarSaleCreateDto dto);
    void MapToCarSaleFromUpdateDto(CarSale entity, CarSaleUpdateDto dto);
    CarSaleReadDto MapToCarSaleReadDto(CarSale entity);
}