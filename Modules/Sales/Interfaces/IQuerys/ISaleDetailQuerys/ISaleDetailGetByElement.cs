using Modules.Sales.Entities;

namespace Modules.Sales.Interfaces.IQuerys.ISaleDetailQuerys;

public interface ISaleDetailGetByElement
{
    SaleDetail GetById(Guid id);
}
