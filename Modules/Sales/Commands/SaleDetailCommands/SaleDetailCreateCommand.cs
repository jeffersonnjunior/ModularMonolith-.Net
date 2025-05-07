using Common.IPersistence.IRepositories;
using Modules.Sales.Entities;
using Modules.Sales.Interfaces.ICommands.ISaleDetailCommands;

namespace Modules.Sales.Commands.SaleDetailCommands;

public class SaleDetailCreateCommand : ISaleDetailCreateCommand
{
    private readonly IBaseRepository _repository;
    public SaleDetailCreateCommand(IBaseRepository repository)
    {
        _repository = repository;
    }
    public SaleDetail Create(SaleDetail saleDetail)
    {
        saleDetail = _repository.Add(saleDetail);
        _repository.SaveChanges();

        return saleDetail;
    }
}