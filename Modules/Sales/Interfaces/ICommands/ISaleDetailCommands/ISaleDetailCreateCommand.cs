using Modules.Sales.Entities;

namespace Modules.Sales.Interfaces.ICommands.ISaleDetailCommands;

public interface ISaleDetailCreateCommand
{
    SaleDetail Create(SaleDetail saleDetail);
}
