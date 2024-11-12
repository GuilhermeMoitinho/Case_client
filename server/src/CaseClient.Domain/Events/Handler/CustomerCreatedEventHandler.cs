using CaseClient.Core.Abstractions;
using CaseClient.Domain.Abstractions;

namespace CaseClient.Domain.Events.Handler;

public class CustomerCreatedEventHandler : IEventHandler<CustomerCreatedEvent>
{
    private readonly ICustomerReadRepository _mongodbRepository;

    public CustomerCreatedEventHandler(ICustomerReadRepository mongodbRepository)
    {
        _mongodbRepository = mongodbRepository;
    }

    public async Task Handle(CustomerCreatedEvent @event, CancellationToken cancellationToken)
    {
        try
        {
            await _mongodbRepository.CreateCustomer(@event.customer);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao inserir no MongoDB: {ex.Message}");
            throw;
        }
    }
}
