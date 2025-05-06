using Common.IPersistence.IRepositories;
using Modules.Sales.Entities;
using Modules.Sales.Interfaces.ICommands.ICarSaleCommands;

namespace Modules.Sales.Commands.CarSaleCommands;

public class CarSaleCreateCommand : ICarSaleCreateCommand
{
    private readonly IBaseRepository _repository;

    public CarSaleCreateCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public CarSale Create(CarSale carSale)
    {
        carSale = _repository.Add(carSale);
        _repository.SaveChanges();

        return carSale;
    }
}