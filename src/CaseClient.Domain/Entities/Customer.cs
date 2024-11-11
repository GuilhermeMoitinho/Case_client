using CaseClient.Core.Abstractions;

namespace CaseClient.Domain.Entities;

public class Customer : Entity, IAggregateRoot
{
    public string CompanyName { get; private set; }

    public SizeCategory CompanySize { get; private set; }

    public Customer(string companyName, SizeCategory companySize)
    {
        CompanyName = companyName;
        CompanySize = companySize;
    }

    protected Customer() { }

    public enum SizeCategory
    {
        Small = 1,
        Medium,
        Large
    }

    public void Update(string companyName, SizeCategory companySize)
    {
        CompanyName = companyName;
        CompanySize = companySize;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateCompanySize(SizeCategory newSize)
    {
        CompanySize = newSize;
    }
}
