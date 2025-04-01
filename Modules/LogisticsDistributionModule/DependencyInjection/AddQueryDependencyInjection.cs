using Microsoft.Extensions.DependencyInjection;
using Modules.LogisticsDistributionModule.Interfaces.IQuery;
using Modules.LogisticsDistributionModule.Queries.Implementations;
using Modules.LogisticsDistributionModule.Query.GetFilter;

namespace Modules.LogisticsDistributionModule.DependencyInjection;

public static class AddQueryDependencyInjection
{
    public static void QueryDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IGetByIdDistributorQuery, GetByIdDistributorQuery>();
        services.AddScoped<IGetFilterDistributorQuery, GetFilterDistributorQuery>();
    }
}