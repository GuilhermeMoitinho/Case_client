using CaseClient.Core.Abstractions;
using CaseClient.Domain.Abstractions;
using CaseClient.Domain.Events;

namespace CaseClient.Infrastructure.EventHandlers;

public class CustomerUpdatedEventHandler : IEventHandler<CustomerUpdatedEvent>
{
    private readonly ICustomerReadRepository _mongodbRepository;

    public CustomerUpdatedEventHandler(ICustomerReadRepository mongodbRepository)
    {
        _mongodbRepository = mongodbRepository;
    }

    public async Task Handle(CustomerUpdatedEvent @event, CancellationToken cancellationToken)
    {
        try
        {
            await _mongodbRepository.UpdateCustomer(@event.customer);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar no MongoDB: {ex.Message}");
            throw;
        }
    }
}
