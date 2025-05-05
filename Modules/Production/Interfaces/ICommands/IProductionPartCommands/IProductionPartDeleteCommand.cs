using Modules.Production.Entities;

namespace Modules.Production.Interfaces.ICommands.IProductionPartCommands;

public interface IProductionPartDeleteCommand
{
    void Delete(ProductionPart productionPart);
}
