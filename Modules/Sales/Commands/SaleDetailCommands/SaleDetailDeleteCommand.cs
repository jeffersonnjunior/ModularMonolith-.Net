using Common.IPersistence.IRepositories;
using Modules.Sales.Entities;
using Modules.Sales.Interfaces.ICommands.ISaleDetailCommands;

namespace Modules.Sales.Commands.SaleDetailCommands;

public class SaleDetailDeleteCommand : ISaleDetailDeleteCommand
{
    private readonly IBaseRepository _repository;

    public SaleDetailDeleteCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public void Delete(SaleDetail saleDetail)
    {
        _repository.Delete(saleDetail);
        _repository.SaveChanges();
    }
}