using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CaseClient.Domain.Entities;

namespace CaseClient.Data.Mappings;

internal class CustomerMapping : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.CompanyName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("CompanyName");

        builder.Property(c => c.CompanySize)
            .IsRequired()
            .HasColumnName("CompanySize");

        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasColumnName("CreatedAt");

        builder.Property(c => c.UpdatedAt)
            .HasColumnName("UpdatedAt");
    }
}
