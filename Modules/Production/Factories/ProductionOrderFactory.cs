using Modules.Production.Dtos.ProductionOrderDtos;
using Modules.Production.Entities;
using Modules.Production.Enums;
using Modules.Production.Interfaces.IFactories;

namespace Modules.Production.Factories;

public class ProductionOrderFactory : IProductionOrderFactory
{
    public ProductionOrder CreateProductionOrder()
    {
        return new ProductionOrder
        {
            Id = Guid.NewGuid(),
            Model = string.Empty,
            ProductionStatus = ProductionStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            CompletedAt = null,
            Parts = new List<ProductionPart>()
        };
    }

    public ProductionOrder MapToProductionOrder(ProductionOrderCreateDto dto)
    {
        return new ProductionOrder
        {
            Id = Guid.NewGuid(),
            Model = dto.Model,
            ProductionStatus = dto.ProductionStatus,
            CreatedAt = dto.CreatedAt,
            CompletedAt = dto.CompletedAt,
            Parts = new List<ProductionPart>()
        };
    }

    public void MapToProductionOrderFromUpdateDto(ProductionOrder entity, ProductionOrderUpdateDto dto)
    {
        entity.Model = dto.Model;
        entity.ProductionStatus = dto.ProductionStatus;
        entity.CompletedAt = dto.CompletedAt;
    }

    public ProductionOrderReadDto MapToProductionOrderReadDto(ProductionOrder entity)
    {
        return new ProductionOrderReadDto
        {
            Id = entity.Id,
            Model = entity.Model,
            ProductionStatus = entity.ProductionStatus,
            CreatedAt = entity.CreatedAt,
            CompletedAt = entity.CompletedAt,
            Parts = entity.Parts?.ToList() ?? new List<ProductionPart>()
        };
    }
}