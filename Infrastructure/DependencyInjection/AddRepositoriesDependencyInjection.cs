using Common.IPersistence.IRepositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Infrastructure.DependencyInjection;

public static class AddRepositoriesDependencyInjection
{
    public static void RepositoriesDependencyInjection(this IServiceCollection repository)
    {
        repository.AddScoped<IBaseRepository, BaseRepository>();
    }
}
