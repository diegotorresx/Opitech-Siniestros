using MediatR;
using Siniestros.Domain.Accidents;

namespace Siniestros.Application.Accidents.Create;

public sealed record CreateAccidentCommand(
    DateTimeOffset OccurredAt,
    string Department,
    string City,
    AccidentType Type,
    List<VehicleRequest> Vehicles,
    int VictimsCount,
    string? Description
) : IRequest<Guid>;

public sealed record VehicleRequest(string Type, string? Plate);
