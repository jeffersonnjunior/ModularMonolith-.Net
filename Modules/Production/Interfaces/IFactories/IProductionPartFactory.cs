using Modules.Production.Dtos.ProductionPartDtos;
using Modules.Production.Entities;

namespace Modules.Production.Interfaces.IFactories;

public interface IProductionPartFactory
{
    ProductionPart CreateProductionPart();
    ProductionPart MapToProductionPart(ProductionPartCreateDto dto);
    void MapToProductionPartFromUpdateDto(ProductionPart entity, ProductionPartUpdateDto dto);
    ProductionPartReadDto MapToProductionOrderReadDto(ProductionPart entity);
}
