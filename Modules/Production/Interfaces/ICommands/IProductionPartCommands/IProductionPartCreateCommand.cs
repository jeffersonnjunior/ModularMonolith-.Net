using Modules.Production.Entities;

namespace Modules.Production.Interfaces.ICommands.IProductionPartCommands;

public interface IProductionPartCreateCommand
{
    ProductionPart Create(ProductionPart productionPart);
}
