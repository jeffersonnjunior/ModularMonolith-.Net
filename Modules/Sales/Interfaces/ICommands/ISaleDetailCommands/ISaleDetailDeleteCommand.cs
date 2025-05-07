using Modules.Sales.Entities;

namespace Modules.Sales.Interfaces.ICommands.ISaleDetailCommands;

public interface ISaleDetailDeleteCommand
{
    void Delete(SaleDetail saleDetail);
}