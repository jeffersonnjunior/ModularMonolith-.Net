using Modules.Sales.Entities;

namespace Modules.Sales.Interfaces.IQuerys.ISaleDetailQuerys;

internal interface ISaleDetailGetByElement
{
    SaleDetail GetById(Guid id);
}
