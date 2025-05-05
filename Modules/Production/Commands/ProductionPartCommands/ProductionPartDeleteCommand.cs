using Common.IPersistence.IRepositories;
using Modules.Production.Entities;
using Modules.Production.Interfaces.ICommands.IProductionPartCommands;

namespace Modules.Production.Commands.ProductionPartCommands;

public class ProductionPartDeleteCommand : IProductionPartDeleteCommand
{
    private readonly IBaseRepository _repository;

    public ProductionPartDeleteCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public void Delete(ProductionPart productionPart)
    {
        _repository.Delete(productionPart);
        _repository.SaveChanges();
    }
}
