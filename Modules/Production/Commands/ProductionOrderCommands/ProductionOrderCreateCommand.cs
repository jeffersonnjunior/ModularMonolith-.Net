using Common.IPersistence.IRepositories;
using Modules.Inventory;
using Modules.Production.Entities;
using Modules.Production.Interfaces.ICommands.IProductionOrderCommands;

namespace Modules.Production.Commands.ProductionOrderCommands;

public class ProductionOrderCreateCommand : IProductionOrderCreateCommand
{
    private readonly IBaseRepository _repository;

    public ProductionOrderCreateCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public ProductionOrder Create(ProductionOrder productionOrder)
    {
        productionOrder = _repository.Add(productionOrder);
        _repository.SaveChanges();

        return productionOrder;
    }
}
