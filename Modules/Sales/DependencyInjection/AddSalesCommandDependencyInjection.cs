using Microsoft.Extensions.DependencyInjection;
using Modules.Sales.Commands.CarSaleCommands;
using Modules.Sales.Interfaces.ICommands.ICarSaleCommands;

namespace Modules.Sales.DependencyInjection;

public static class AddSalesCommandDependencyInjection
{
    public static void SalesCommandDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICarSaleCreateCommand, CarSaleCreateCommand>();
        services.AddScoped<ICarSaleDeleteCommand, CarSaleDeleteCommand>();
        services.AddScoped<ICarSaleUpdateCommand, CarSaleUpdateCommand>();
    }
}