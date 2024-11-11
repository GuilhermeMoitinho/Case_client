using CaseClient.Core.Data;
using CaseClient.Data.CustomerContext;

namespace CaseClient.Data;

public class UnityOfWork : IUnitOfWork, IDisposable
{
    private readonly PostgreSqlDbContext _postgreSqlDbContext;

    public UnityOfWork(PostgreSqlDbContext PostgreSqlDbContext) => _postgreSqlDbContext = PostgreSqlDbContext;

    public async Task<bool> Commit() => await _postgreSqlDbContext.SaveChangesAsync() > 0;

    public void Dispose() => _postgreSqlDbContext.Dispose();
}
