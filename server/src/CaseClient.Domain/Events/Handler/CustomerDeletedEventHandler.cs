using CaseClient.Core.Abstractions;
using CaseClient.Domain.Abstractions;
using CaseClient.Domain.Events;

namespace CaseClient.Infrastructure.EventHandlers;

public class CustomerDeletedEventHandler : IEventHandler<CustomerDeletedEvent>
{
    private readonly ICustomerReadRepository _mongodbRepository;

    public CustomerDeletedEventHandler(ICustomerReadRepository mongodbRepository)
    {
        _mongodbRepository = mongodbRepository;
    }

    public async Task Handle(CustomerDeletedEvent @event, CancellationToken cancellationToken)
    {
        try
        {
            await _mongodbRepository.RemoveCustomer(@event.customer);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao deletar no MongoDB: {ex.Message}");
            throw;
        }
    }
}
