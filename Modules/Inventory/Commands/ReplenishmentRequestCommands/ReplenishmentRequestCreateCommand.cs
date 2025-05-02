using Common.IPersistence.IRepositories;
using Modules.Inventory.Entities;
using Modules.Inventory.Interfaces.ICommands.IReplenishmentRequestCommands;

namespace Modules.Inventory.Commands.ReplenishmentRequestCommands;

public class ReplenishmentRequestCreateCommand : IReplenishmentRequestCreateCommand
{
    private readonly IBaseRepository _repository;

    public ReplenishmentRequestCreateCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public ReplenishmentRequest Add(ReplenishmentRequest replenishmentRequest)
    {
        replenishmentRequest = _repository.Add(replenishmentRequest);
        _repository.SaveChanges();

        return replenishmentRequest;
    }
}