using MediatR;
using Siniestros.Application.Accidents.Abstractions;
using Siniestros.Domain.Accidents;

namespace Siniestros.Application.Accidents.Create;

public sealed class CreateAccidentCommandHandler : IRequestHandler<CreateAccidentCommand, Guid>
{
    private readonly IAccidentRepository _repo;

    public CreateAccidentCommandHandler(IAccidentRepository repo) => _repo = repo;

    public async Task<Guid> Handle(CreateAccidentCommand request, CancellationToken ct)
    {
        var vehicles = request.Vehicles.Select(v => VehicleInvolved.Create(v.Type, v.Plate));

        var accident = Accident.Create(
            request.OccurredAt,
            request.Department,
            request.City,
            request.Type,
            vehicles,
            request.VictimsCount,
            request.Description);

        await _repo.AddAsync(accident, ct);
        return accident.Id.Value;
    }
}
