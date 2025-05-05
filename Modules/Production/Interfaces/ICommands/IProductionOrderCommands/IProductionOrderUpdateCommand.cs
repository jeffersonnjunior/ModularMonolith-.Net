using Modules.Production.Entities;

namespace Modules.Production.Interfaces.ICommands.IProductionOrderCommands;

public interface IProductionOrderUpdateCommand
{
    void Update(ProductionOrder productionOrder);
}
