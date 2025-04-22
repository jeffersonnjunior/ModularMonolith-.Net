using Microsoft.Extensions.DependencyInjection;
using Modules.QualityInspectionModule.Command.Create;
using Modules.QualityInspectionModule.Command.Delete;
using Modules.QualityInspectionModule.Command.Update;
using Modules.QualityInspectionModule.Interfaces.ICommand.ICreate;
using Modules.QualityInspectionModule.Interfaces.ICommand.IDelete;
using Modules.QualityInspectionModule.Interfaces.ICommand.IUpdate;

namespace Modules.QualityInspectionModule.DependencyInjection;

public static class AddCommandDependencyInjection
{
    public static void CommandDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICreateInspectionCommand, CreateInspectionCommand>();
        services.AddScoped<IDeleteInspectionCommand, DeleteInspectionCommand>();
        services.AddScoped<IUpdateInspectionCommand, UpdateInspectionCommand>();
        services.AddScoped<ICreateInspectionFailureCommand, CreateInspectionFailureCommand>();
        services.AddScoped<IDeleteInspectionFailureCommand, DeleteInspectionFailureCommand>();
        services.AddScoped<IUpdateInspectionFailureCommand, UpdateInspectionFailureCommand>();
    }
}