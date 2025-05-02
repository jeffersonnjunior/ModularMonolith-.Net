using Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Api.Filters;

public class NotificationActionFilter : IActionFilter
{
    private readonly NotificationContext _notificationContext;

    public NotificationActionFilter(NotificationContext notificationContext)
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
