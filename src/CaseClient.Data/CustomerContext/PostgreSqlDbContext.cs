using CaseClient.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseClient.Data.CustomerContext;

public class PostgreSqlDbContext : DbContext
{
    public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
}