using Common.IException;
using Infrastructure.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class NotificationActionFilter : IActionFilter
{
    private readonly INotificationContext _notificationContext;

    public NotificationActionFilter(INotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    { 
        //if (!context.HttpContext.Request.Headers.ContainsKey("Authorization")) context.Result = new UnauthorizedObjectResult("Autorização Ausente");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (_notificationContext.HasNotifications()) context.Result = new BadRequestObjectResult(_notificationContext.GetNotifications());
    }
}