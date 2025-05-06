using Common.IPersistence.IRepositories;
using Modules.Sales.Entities;
using Modules.Sales.Interfaces.ICommands.ICarSaleCommands;

namespace Modules.Sales.Commands.CarSaleCommands;

public class CarSaleUpdateCommand : ICarSaleUpdateCommand
{
    private readonly IBaseRepository _repository;

    public CarSaleUpdateCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public void Update(CarSale carSale)
    {
        _repository.Update(carSale);
        _repository.SaveChanges();
    }
}
