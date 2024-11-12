using MediatR;

namespace CaseClient.Core.Abstractions;

public interface IEventHandler<T> : INotificationHandler<T> where T : IEvent;
