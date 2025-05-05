using Common.IPersistence.IRepositories;
using Modules.Production.Entities;
using Modules.Production.Interfaces.ICommands.IProductionPartCommands;

namespace Modules.Production.Commands.ProductionPartCommands;

public class ProductionPartUpdateCommand : IProductionPartUpdateCommand
{
    private readonly IBaseRepository _repository;

    public ProductionPartUpdateCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public void Update(ProductionPart productionPart)
    {
        _repository.Update(productionPart);
        _repository.SaveChanges();
    }
}