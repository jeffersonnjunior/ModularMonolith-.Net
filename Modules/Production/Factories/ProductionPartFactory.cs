using Modules.Production.Dtos.ProductionPartDtos;
using Modules.Production.Entities;
using Modules.Production.Interfaces.IFactories;

namespace Modules.Production.Factories;

public class ProductionPartFactory : IProductionPartFactory
{
    public ProductionPart CreateProductionPart()
    {
        return new ProductionPart
        {
            Id = Guid.NewGuid(),
            ProductionOrderId = Guid.Empty,
            PartCode = string.Empty,
            QuantityUsed = 0,
            ProductionOrder = null
        };
    }

    public ProductionPart MapToProductionPart(ProductionPartCreateDto dto)
    {
        return new ProductionPart
        {
            Id = Guid.NewGuid(),
            ProductionOrderId = dto.ProductionOrderId,
            PartCode = dto.PartCode,
            QuantityUsed = dto.QuantityUsed,
            ProductionOrder = null
        };
    }

    public void MapToProductionPartFromUpdateDto(ProductionPart entity, ProductionPartUpdateDto dto)
    {
        entity.Id = dto.Id;
        entity.ProductionOrderId = dto.ProductionOrderId;
        entity.PartCode = dto.PartCode;
        entity.QuantityUsed = dto.QuantityUsed;
    }

    public ProductionPartReadDto MapToProductionOrderReadDto(ProductionPart entity)
    {
        return new ProductionPartReadDto
        {
            Id = entity.Id,
            ProductionOrderId = entity.ProductionOrderId,
            PartCode = entity.PartCode,
            QuantityUsed = entity.QuantityUsed,
            ProductionOrder = entity.ProductionOrder
        };
    }
}