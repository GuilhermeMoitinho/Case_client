using static CaseClient.Domain.Entities.Customer;

namespace CaseClient.Application.ViewModels;

public record CustomerViewModel(Guid Id, string CompanyName, SizeCategory CompanySize);

