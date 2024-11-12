using CaseClient.Core.Abstractions;

namespace CaseClient.Core.Data;

public interface IRepository<T> where T : IAggregateRoot { }

