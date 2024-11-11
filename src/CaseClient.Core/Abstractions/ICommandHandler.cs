using MediatR;

namespace CaseClient.Core.Abstractions;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand> where TCommand : ICommand;
