using Modules.Production.Dtos.ProductionOrderDtos;
using Modules.Production.Entities;

namespace Modules.Production.Interfaces.IFactories;

public interface IProductionOrderFactory
{
    ProductionOrder CreateProductionOrder();
    ProductionOrder MapToProductionOrder(ProductionOrderCreateDto dto);
    void MapToProductionOrderFromUpdateDto(ProductionOrder entity, ProductionOrderUpdateDto dto);
    ProductionOrderReadDto MapToProductionOrderReadDto(ProductionOrder entity);
}
