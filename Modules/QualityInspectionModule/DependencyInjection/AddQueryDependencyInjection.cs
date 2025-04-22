using Microsoft.Extensions.DependencyInjection;
using Modules.QualityInspectionModule.Interfaces.IQuery.IGetById;
using Modules.QualityInspectionModule.Interfaces.IQuery.IGetFilter;
using Modules.QualityInspectionModule.Query.GetById;
using Modules.QualityInspectionModule.Query.GetFilter;

namespace Modules.QualityInspectionModule.DependencyInjection;

public static class AddQueryDependencyInjection
{
    public static void QueryDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IGetByIdInspectionFailureQuery, GetByIdInspectionFailureQuery>();
        services.AddScoped<IGetFilterInspectionFailureQuery, GetFilterInspectionFailureQuery>();
        services.AddScoped<IGetByIdInspectionQuery, GetByIdInspectionQuery>();
        services.AddScoped<IGetFilterInspectionQuery, GetFilterInspectionQuery>();
    }
}