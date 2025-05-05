using Modules.Production.Entities;

namespace Modules.Production.Interfaces.ICommands.IProductionOrderCommands;

public interface IProductionOrderDeleteCommand
{
    void Delete(ProductionOrder productionOrder);
}
