using Microsoft.EntityFrameworkCore;
using Siniestros.Application.Accidents.Abstractions;
using Siniestros.Domain.Accidents;
using Siniestros.Infrastructure.Persistence;

namespace Siniestros.Infrastructure.Repositories;

public sealed class AccidentRepository : IAccidentRepository
{
    private readonly SiniestrosDbContext _db;

    public AccidentRepository(SiniestrosDbContext db) => _db = db;

    public async Task AddAsync(Accident accident, CancellationToken ct)
    {
        _db.Accidents.Add(accident);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<(IReadOnlyList<Accident> Items, long Total)> SearchAsync(
        string? department,
        DateTimeOffset? from,
        DateTimeOffset? to,
        int pageNumber,
        int pageSize,
        CancellationToken ct)
    {
        var query = _db.Accidents
            .AsNoTracking()
            .Include(a => a.Vehicles)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(department))
        {
            var dep = department.Trim();
            query = query.Where(a => a.Department == dep);
        }

        if (from.HasValue)
            query = query.Where(a => a.OccurredAt >= from.Value);

        if (to.HasValue)
            query = query.Where(a => a.OccurredAt <= to.Value);

        query = query.OrderByDescending(a => a.OccurredAt);

        var total = await query.LongCountAsync(ct);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, total);
    }
}
