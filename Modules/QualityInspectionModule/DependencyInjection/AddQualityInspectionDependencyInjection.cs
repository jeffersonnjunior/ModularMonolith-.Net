using Microsoft.Extensions.DependencyInjection;

namespace Modules.QualityInspectionModule.DependencyInjection;

public static class AddQualityInspectionDependencyInjection
{
    public static void DependencyInjectionQualityInspection(this IServiceCollection services)
    {
        services.CommandDependencyInjection();
        services.QueryDependencyInjection();
    }
}