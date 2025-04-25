using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.DependencyInjection;

public static class AddCommonDependencyInjection
{
    public static void CommonDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.NotificationsDependencyInjection();
    }
}
