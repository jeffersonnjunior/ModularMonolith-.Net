using Microsoft.Extensions.DependencyInjection;
using Modules.Sales.Commands.CarSaleCommands;
using Modules.Sales.Commands.SaleDetailCommands;
using Modules.Sales.Interfaces.ICommands.ICarSaleCommands;
using Modules.Sales.Interfaces.ICommands.ISaleDetailCommands;

namespace Modules.Sales.DependencyInjection;

public static class AddSalesCommandDependencyInjection
{
    public static void SalesCommandDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICarSaleCreateCommand, CarSaleCreateCommand>();
        services.AddScoped<ICarSaleDeleteCommand, CarSaleDeleteCommand>();
        services.AddScoped<ICarSaleUpdateCommand, CarSaleUpdateCommand>();
        services.AddScoped<ISaleDetailCreateCommand, SaleDetailCreateCommand>();
        services.AddScoped<ISaleDetailDeleteCommand, SaleDetailDeleteCommand>();
        services.AddScoped<ISaleDetailUpdateCommand, SaleDetailUpdateCommand>();
    }
}