using Modules.Production.Entities;

namespace Modules.Production.Interfaces.IQuerys.IProductionOrderQuerys;

public interface IProductionOrderGetByElement
{
    ProductionOrder GetById(Guid id);
}