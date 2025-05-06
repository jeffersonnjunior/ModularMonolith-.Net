using Common.IPersistence.IRepositories;
using Modules.Sales.Entities;
using Modules.Sales.Interfaces.ICommands.ICarSaleCommands;

namespace Modules.Sales.Commands.CarSaleCommands;

public class CarSaleDeleteCommand : ICarSaleDeleteCommand
{
    private readonly IBaseRepository _repository;
    public CarSaleDeleteCommand(IBaseRepository repository)
    {
        _repository = repository;
    }
    public void Delete(CarSale carSale)
    {
        _repository.Delete(carSale);
        _repository.SaveChanges();
    }
}