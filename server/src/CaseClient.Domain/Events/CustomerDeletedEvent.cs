using CaseClient.Core.Abstractions;
using CaseClient.Domain.Entities;

namespace CaseClient.Domain.Events;

public record CustomerDeletedEvent(Customer customer) : IEvent;