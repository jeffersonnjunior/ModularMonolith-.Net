using Modules.Sales.Dtos.SaleDetailDtos;

namespace Modules.Sales.Interfaces.IQuerys.ISaleDetailQuerys;

public interface ISaleDetailGetFilter
{
    SaleDetailReturnFilterDto GetFilter(SaleDetailGetFilterDto filter);
}
