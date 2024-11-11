using CaseClient.Core.Abstractions;
using CaseClient.Domain.Abstractions;

namespace CaseClient.Application.Commands;

public record RemoveCustomerCommand(Guid Id) : ICommand;

public class RemoveCustomerCommandHandler : ICommandHandler<RemoveCustomerCommand>
{
    private readonly ICustomerRepository _CustomerRepository;

    public RemoveCustomerCommandHandler(ICustomerRepository CustomerRepository) => _CustomerRepository = CustomerRepository;

    public async Task Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
    {
        var Customer = await _CustomerRepository.GetCustomerById(request.Id);

        await _CustomerRepository.RemoveCustomer(Customer);
    }
}

