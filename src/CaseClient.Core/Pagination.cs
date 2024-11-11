namespace CaseClient.Core;

public record Pagination(int limit, int offset)
{
    public int Limit { get; } = limit;
    public int Offset { get; } = offset;
}
