using Common.IException;
using Infrastructure.Exception;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class AddNotificationDependencyInjection
{
    public static void NotificationDependencyInjection(this IServiceCollection repository)
    {
        repository.AddScoped<INotificationContext, NotificationContext>();
    }
}