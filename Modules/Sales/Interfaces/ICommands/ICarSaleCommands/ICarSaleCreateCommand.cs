using Modules.Production.Entities;
using Modules.Sales.Entities;

namespace Modules.Sales.Interfaces.ICommands.ICarSaleCommands;

public interface ICarSaleCreateCommand
{
    CarSale Create(CarSale carSale);
}
