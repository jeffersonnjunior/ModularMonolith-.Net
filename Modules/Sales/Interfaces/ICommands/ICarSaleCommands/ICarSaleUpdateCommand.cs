using Modules.Sales.Entities;

namespace Modules.Sales.Interfaces.ICommands.ICarSaleCommands;

public interface ICarSaleUpdateCommand
{
    void Update(CarSale carSale);
}
