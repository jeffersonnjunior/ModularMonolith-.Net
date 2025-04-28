using Common.ICache.Services;
using Infrastructure.Cache.Connection;
using Infrastructure.Cache.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class AddCacheDependencyInjection
{
    public static IServiceCollection CacheDependencyInjection(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddSingleton<RedisFactory>(_ =>
            new RedisFactory(configuration.GetConnectionString("Redis")));

        services.AddScoped<ICacheService, CacheService>();

        return services;
    }
}