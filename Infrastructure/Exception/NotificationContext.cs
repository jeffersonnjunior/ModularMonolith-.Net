using Common.IException;

namespace Infrastructure.Exception;

public class NotificationContext : INotificationContext
{
    private readonly List<NotificationModel> _notifications;

    public NotificationContext()
    {
        _notifications = new List<NotificationModel>();
    }

    public IReadOnlyCollection<NotificationModel> GetNotifications() => _notifications.AsReadOnly();
    public void AddNotification(string message) => _notifications.Add(new NotificationModel(message));
    public bool HasNotifications() => _notifications.Count > 0;
}