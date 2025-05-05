using Common.IPersistence.IRepositories;
using Modules.Production.Entities;
using Modules.Production.Interfaces.ICommands.IProductionOrderCommands;

namespace Modules.Production.Commands.ProductionOrderCommands;

public class ProductionOrderUpdateCommand : IProductionOrderUpdateCommand
{
    private readonly IBaseRepository _repository;

    public ProductionOrderUpdateCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public void Update(ProductionOrder productionOrder)
    {
        _repository.Update(productionOrder);
        _repository.SaveChanges();
    }
}