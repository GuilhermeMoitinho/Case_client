using Microsoft.AspNetCore.Mvc;
using CaseClient.Application.Notifications.Abstractions;

namespace CaseClient.WebAPI.Controllers.Base;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotifier _notifier;

    protected MainController(INotifier notifier) => _notifier = notifier;
        
    protected bool OperationIsValid()
    {
        return !_notifier.HasNotification();
    }

    protected IEnumerable<string> ReturnErrors()
    {
        return  _notifier.GetNotification().Select(n => n.Message);
    }
}
