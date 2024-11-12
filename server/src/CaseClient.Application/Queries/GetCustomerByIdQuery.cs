using CaseClient.Application.Notifications;
using CaseClient.Application.Notifications.Abstractions;
using CaseClient.Core.Abstractions;
using CaseClient.Domain.Abstractions;
using CaseClient.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CaseClient.Application.Queries;

public record GetCustomerByIdQuery(Guid Id) : IQuery<Customer>;

public class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, Customer>
{
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly INotifier _notifier;
    private readonly IMemoryCache _cache;

    public GetCustomerByIdQueryHandler(
        ICustomerReadRepository customerReadRepository,
        INotifier notifier,
        IMemoryCache cache)
    {
        _customerReadRepository = customerReadRepository;
        _notifier = notifier;
        _cache = cache;
    }

    public async Task<Customer> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = $"Customer_{query.Id}";
        var customer = await CacheHelper.GetOrSetCacheAsync(
            _cache,
            cacheKey,
            () => _customerReadRepository.GetCustomerById(query.Id)
        );

        if (customer == null)
        {
            _notifier.Add(new Notification("Customer not found."));
        }

        return customer;
    }
}
