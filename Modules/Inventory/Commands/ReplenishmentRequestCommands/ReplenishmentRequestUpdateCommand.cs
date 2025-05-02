using Common.IPersistence.IRepositories;
using Modules.Inventory.Entities;
using Modules.Inventory.Interfaces.ICommands.IReplenishmentRequestCommands;

namespace Modules.Inventory.Commands.ReplenishmentRequestCommands;

public class ReplenishmentRequestUpdateCommand : IReplenishmentRequestUpdateCommand
{
    private readonly IBaseRepository _repository;

    public ReplenishmentRequestUpdateCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public void Update(ReplenishmentRequest replenishmentRequest)
    {
        _repository.Update(replenishmentRequest);
        _repository.SaveChanges();
    }
}