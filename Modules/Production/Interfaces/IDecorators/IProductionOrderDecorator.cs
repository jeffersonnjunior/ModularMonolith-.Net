using Modules.Production.Dtos.ProductionOrderDtos;

namespace Modules.Production.Interfaces.IDecorators;

public interface IProductionOrderDecorator
{
    ProductionOrderReadDto GetById(Guid id);
    ProductionOrderReturnFilterDto GetFilter(ProductionOrderGetFilterDto filter);
    ProductionOrderReadDto Create(ProductionOrderCreateDto productionOrderCreateDto);
    void Update(ProductionOrderUpdateDto productionOrderUpdateDto);
    void Delete(Guid id);
}
