namespace CaseClient.Core.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}