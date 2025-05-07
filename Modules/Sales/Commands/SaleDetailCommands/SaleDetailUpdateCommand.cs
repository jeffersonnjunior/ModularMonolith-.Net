using Common.IPersistence.IRepositories;
using Modules.Sales.Entities;
using Modules.Sales.Interfaces.ICommands.ISaleDetailCommands;

namespace Modules.Sales.Commands.SaleDetailCommands;
public class SaleDetailUpdateCommand : ISaleDetailUpdateCommand
{
    private readonly IBaseRepository _repository;

    public SaleDetailUpdateCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public void Update(SaleDetail saleDetail)
    {
        _repository.Update(saleDetail);
        _repository.SaveChanges();
    }
}
