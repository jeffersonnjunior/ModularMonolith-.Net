using Common.Data;
using Infrastructure.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class AddRepositoryDependencyInjection
{
    public static void RepositorysDependencyInjection(this IServiceCollection repository)
    {
        repository.AddScoped<IBaseRepository, BaseRepository>();
    }
}