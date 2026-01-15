namespace Siniestros.Application.Common.Paging;

public sealed record PagedResult<T>(
    IReadOnlyList<T> Items,
    int PageNumber,
    int PageSize,
    long TotalItems)
{
    public long TotalPages =>
        TotalItems == 0 ? 0 : (long)Math.Ceiling(TotalItems / (double)PageSize);
}
