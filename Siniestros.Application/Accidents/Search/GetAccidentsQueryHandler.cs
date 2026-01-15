using MediatR;
using Siniestros.Application.Accidents.Abstractions;
using Siniestros.Application.Common.Paging;

namespace Siniestros.Application.Accidents.Search;

public sealed class GetAccidentsQueryHandler : IRequestHandler<GetAccidentsQuery, PagedResult<AccidentDto>>
{
    private readonly IAccidentRepository _repo;

    public GetAccidentsQueryHandler(IAccidentRepository repo) => _repo = repo;

    public async Task<PagedResult<AccidentDto>> Handle(GetAccidentsQuery request, CancellationToken ct)
    {
        var (items, total) = await _repo.SearchAsync(
            request.Department,
            request.From,
            request.To,
            request.PageNumber,
            request.PageSize,
            ct);

        var dtos = items.Select(a => new AccidentDto(
            a.Id.Value,
            a.OccurredAt,
            a.Department,
            a.City,
            a.Type.ToString(),
            a.VictimsCount,
            a.Vehicles.Select(v => new VehicleDto(v.Type, v.Plate)).ToList(),
            a.Description
        )).ToList();

        return new PagedResult<AccidentDto>(dtos, request.PageNumber, request.PageSize, total);
    }
}
