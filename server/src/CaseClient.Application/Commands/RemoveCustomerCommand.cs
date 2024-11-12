using CaseClient.Application.Notifications.Abstractions;
using CaseClient.Application.Notifications;
using CaseClient.Core.Abstractions;
using CaseClient.Core.Data;
using CaseClient.Domain.Abstractions;
using CaseClient.Domain.Entities;
using CaseClient.Domain.Events;
using MediatR;

namespace CaseClient.Application.Commands;

public record RemoveCustomerCommand(Guid Id) : ICommand;

public class RemoveCustomerCommandHandler : ICommandHandler<RemoveCustomerCommand>
{
    private readonly ICustomerWriteRepository _customerWriteRepository;
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly IMediator _mediator;
    private IUnitOfWork _unitOfWork;
    private readonly INotifier _notifier;  

    public RemoveCustomerCommandHandler(ICustomerWriteRepository customerWriteRepository,
                                        IUnitOfWork unitOfWork,
                                        ICustomerReadRepository customerReadRepository,
                                        IMediator mediator,
                                        INotifier notifier)
    {
        _customerWriteRepository = customerWriteRepository;
        _unitOfWork = unitOfWork;
        _customerReadRepository = customerReadRepository;
        _mediator = mediator;
        _notifier = notifier; 
    }

    public async Task Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerReadRepository.GetCustomerById(request.Id);
        if (customer == null)
        {
            _notifier.Add(new Notification("Customer not found."));
            return;  
        }

        await _customerWriteRepository.RemoveCustomer(customer);

        await _mediator.Publish(new CustomerDeletedEvent(customer));

        await _unitOfWork.Commit();
    }
}

