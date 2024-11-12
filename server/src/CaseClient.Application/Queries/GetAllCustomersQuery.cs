using CaseClient.Application.Notifications;
using CaseClient.Application.Notifications.Abstractions;
using CaseClient.Core;
using CaseClient.Core.Abstractions;
using CaseClient.Domain.Abstractions;
using CaseClient.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CaseClient.Application.Queries;

public record GetAllCustomersQuery(Pagination pagination) : IQuery<IEnumerable<Customer>>;

public class GetAllCustomersQueryHandler : IQueryHandler<GetAllCustomersQuery, IEnumerable<Customer>>
{
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly INotifier _notifier;
    private readonly IMemoryCache _cache;

    public GetAllCustomersQueryHandler(
        ICustomerReadRepository customerReadRepository,
        INotifier notifier,
        IMemoryCache cache)
    {
        _customerReadRepository = customerReadRepository;
        _notifier = notifier;
        _cache = cache;
    }

    public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = $"AllCustomers_{query.pagination.Limit}_{query.pagination.Offset}";
        var customers = await CacheHelper.GetOrSetCacheAsync(
            _cache,
            cacheKey,
            () => _customerReadRepository.GetAllCustomer(query.pagination)
        );

        if (customers == null || !customers.Any())
        {
            _notifier.Add(new Notification("No customers found."));
        }

        return customers;
    }
}
