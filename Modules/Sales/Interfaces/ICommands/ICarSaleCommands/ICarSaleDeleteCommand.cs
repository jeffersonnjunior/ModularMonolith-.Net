using Modules.Sales.Entities;

namespace Modules.Sales.Interfaces.ICommands.ICarSaleCommands;

public interface ICarSaleDeleteCommand
{
    void Delete(CarSale carSale);
}
