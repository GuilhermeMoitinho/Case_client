using MediatR;

namespace CaseClient.Core.Abstractions;

public interface ICommand : IRequest;

public interface ICommand<TResult> : IRequest<TResult>;