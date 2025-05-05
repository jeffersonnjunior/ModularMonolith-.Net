using Modules.Production.Entities;

namespace Modules.Production.Interfaces.ICommands.IProductionOrderCommands;

public interface IProductionOrderCreateCommand
{
    ProductionOrder Create(ProductionOrder productionOrder);
}
