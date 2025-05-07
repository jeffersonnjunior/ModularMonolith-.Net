using Modules.Sales.Entities;

namespace Modules.Sales.Interfaces.ICommands.ISaleDetailCommands;

public interface ISaleDetailUpdateCommand
{
    void Update(SaleDetail saleDetail);
}
