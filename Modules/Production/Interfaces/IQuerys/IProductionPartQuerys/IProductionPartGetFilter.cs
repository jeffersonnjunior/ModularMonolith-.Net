using Modules.Production.Dtos.ProductionPartDtos;

namespace Modules.Production.Interfaces.IQuerys.IProductionPartQuerys;

public interface IProductionPartGetFilter
{
    ProductionPartReturnFilterDto GetFilter(ProductionPartGetFilterDto filter);
}
