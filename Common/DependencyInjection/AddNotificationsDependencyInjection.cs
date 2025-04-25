using Common.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Common.DependencyInjection;

public static class AddNotificationsDependencyInjection
{
    public static void NotificationsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<NotificationContext>();
    }
}
