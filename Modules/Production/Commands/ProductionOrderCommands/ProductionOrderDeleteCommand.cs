using Common.IPersistence.IRepositories;
using Modules.Production.Entities;
using Modules.Production.Interfaces.ICommands.IProductionOrderCommands;

namespace Modules.Production.Commands.ProductionOrderCommands;

public class ProductionOrderDeleteCommand : IProductionOrderDeleteCommand
{
    private readonly IBaseRepository _repository;

    public ProductionOrderDeleteCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public void Delete(ProductionOrder productionOrder)
    {
        _repository.Delete(productionOrder);
        _repository.SaveChanges();
    }
}