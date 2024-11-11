using static CaseClient.Domain.Entities.Customer;

namespace CaseClient.Application.ViewModels;

public record CustomerRequest(string CompanyName, SizeCategory CompanySize);

