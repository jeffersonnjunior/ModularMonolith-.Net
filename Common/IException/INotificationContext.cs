namespace Common.IException;

public interface INotificationContext
{
    IReadOnlyCollection<NotificationModel> GetNotifications();
    void AddNotification(string message);
    bool HasNotifications();
}