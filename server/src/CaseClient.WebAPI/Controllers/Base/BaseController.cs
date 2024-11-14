using Microsoft.AspNetCore.Mvc;
using CaseClient.Application.Notifications.Abstractions;

namespace CaseClient.WebAPI.Controllers.Base;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotifier _notifier;

    protected MainController(INotifier notifier) => _notifier = notifier;
        
    protected bool OperationIsValid() => !_notifier.HasNotification();
         

    protected IEnumerable<string> ReturnErrors() => _notifier.GetNotification().Select(n => n.Message);
          
}
