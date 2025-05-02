using Common.IPersistence.IRepositories;
using Modules.Inventory.Entities;
using Modules.Inventory.Interfaces.ICommands.IReplenishmentRequestCommands;

namespace Modules.Inventory.Commands.ReplenishmentRequestCommands;

public class ReplenishmentRequestDeleteCommand : IReplenishmentRequestDeleteCommand
{
    private readonly IBaseRepository _repository;

    public ReplenishmentRequestDeleteCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public void Delete(ReplenishmentRequest replenishmentRequest)
    {
        _repository.Delete(replenishmentRequest);
        _repository.SaveChanges();
    }
}