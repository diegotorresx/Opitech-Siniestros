using MediatR;
using Siniestros.Application.Common.Paging;

namespace Siniestros.Application.Accidents.Search;

public sealed record GetAccidentsQuery(
    string? Department,
    DateTimeOffset? From,
    DateTimeOffset? To,
    int PageNumber = 1,
    int PageSize = 20
) : IRequest<PagedResult<AccidentDto>>;

public sealed record AccidentDto(
    Guid Id,
    DateTimeOffset OccurredAt,
    string Department,
    string City,
    string Type,
    int VictimsCount,
    IReadOnlyList<VehicleDto> Vehicles,
    string? Description
);

public sealed record VehicleDto(string Type, string? Plate);
