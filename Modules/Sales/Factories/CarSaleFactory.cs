using Modules.Sales.Dtos.CarSaleDtos;
using Modules.Sales.Entities;
using Modules.Sales.Enums;
using Modules.Sales.Interfaces.IFactories;

namespace Modules.Sales.Factories;

public class CarSaleFactory : ICarSaleFactory
{
    public CarSale CreateCarSale()
    {
        return new CarSale
        {
            Id = Guid.NewGuid(),
            ProductionOrderId = Guid.Empty,
            Status = SaleStatus.Pendente,
            SoldAt = null,
        };
    }

    public CarSale MapToCarSale(CarSaleCreateDto dto)
    {
        return new CarSale
        {
            Id = Guid.NewGuid(),
            ProductionOrderId = dto.ProductionOrderId,
            Status = dto.Status,
            SoldAt = dto.SoldAt,
        };
    }

    public void MapToCarSaleFromUpdateDto(CarSale entity, CarSaleUpdateDto dto)
    {
        entity.ProductionOrderId = dto.ProductionOrderId;
        entity.Status = dto.Status;
        entity.SoldAt = dto.SoldAt;
    }

    public CarSaleReadDto MapToCarSaleReadDto(CarSale entity)
    {
        return new CarSaleReadDto
        {
            Id = entity.Id,
            ProductionOrderId = entity.ProductionOrderId,
            Status = entity.Status,
            SoldAt = entity.SoldAt,
            SaleDetail = entity.SaleDetail
        };
    }
}