using MediatR;

namespace CaseClient.Core.Abstractions;

public interface IQuery<TResult> : IRequest<TResult>;