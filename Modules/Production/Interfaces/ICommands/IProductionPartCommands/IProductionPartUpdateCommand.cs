using Modules.Production.Entities;

namespace Modules.Production.Interfaces.ICommands.IProductionPartCommands;

public interface IProductionPartUpdateCommand
{
    void Update(ProductionPart productionPart);
}
