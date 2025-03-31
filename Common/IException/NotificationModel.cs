namespace Common.IException;

public class NotificationModel
{
    public string Message { get; }

    public NotificationModel(string message)
    {
        Message = message;
    }
}