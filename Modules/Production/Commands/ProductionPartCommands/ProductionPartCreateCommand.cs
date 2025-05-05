using Common.IPersistence.IRepositories;
using Modules.Production.Entities;
using Modules.Production.Interfaces.ICommands.IProductionPartCommands;

namespace Modules.Production.Commands.ProductionPartCommands;

public class ProductionPartCreateCommand : IProductionPartCreateCommand
{
    private readonly IBaseRepository _repository;

    public ProductionPartCreateCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public ProductionPart Create(ProductionPart productionPart)
    {
        productionPart = _repository.Add(productionPart);
        _repository.SaveChanges();

        return productionPart;
    }
}
