using FluentValidation.Results;

namespace CaseClient.Application.Notifications;

public class Notification : ValidationFailure
{
    public string Message { get; private set; }

    public Notification(string message)
    {
        Message = message;
    }
}
