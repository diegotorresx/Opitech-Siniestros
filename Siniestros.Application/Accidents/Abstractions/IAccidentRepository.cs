using Siniestros.Domain.Accidents;

namespace Siniestros.Application.Accidents.Abstractions;

public interface IAccidentRepository
{
    Task AddAsync(Accident accident, CancellationToken ct);

    Task<(IReadOnlyList<Accident> Items, long Total)> SearchAsync(
        string? department,
        DateTimeOffset? from,
        DateTimeOffset? to,
        int pageNumber,
        int pageSize,
        CancellationToken ct);
}
