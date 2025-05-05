using Modules.Production.Dtos.ProductionOrderDtos;

namespace Modules.Production.Interfaces.IQuerys.IProductionOrderQuerys;

public interface IProductionOrderGetFilter
{
    ProductionOrderReturnFilterDto GetFilter(ProductionOrderGetFilterDto filter);
}
