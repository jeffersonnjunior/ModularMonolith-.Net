using Modules.Production.Entities;

namespace Modules.Production.Interfaces.IQuerys.IProductionPartQuerys;

public interface IProductionPartGetByElement
{
    ProductionPart GetById(Guid id);
}
