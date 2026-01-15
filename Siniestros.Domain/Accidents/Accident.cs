using Siniestros.Domain.Common;

namespace Siniestros.Domain.Accidents;

public sealed class Accident : Entity<AccidentId>
{
    private readonly List<VehicleInvolved> _vehicles = new();

    public DateTimeOffset OccurredAt { get; private set; }
    public string Department { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public AccidentType Type { get; private set; }
    public IReadOnlyCollection<VehicleInvolved> Vehicles => _vehicles.AsReadOnly();
    public int VictimsCount { get; private set; }
    public string? Description { get; private set; }

    private Accident() { } // EF

    public static Accident Create(
        DateTimeOffset occurredAt,
        string department,
        string city,
        AccidentType type,
        IEnumerable<VehicleInvolved> vehicles,
        int victimsCount,
        string? description)
    {
        if (occurredAt == default)
            throw new DomainException("OccurredAt is required.");

        if (string.IsNullOrWhiteSpace(department))
            throw new DomainException("Department is required.");

        if (string.IsNullOrWhiteSpace(city))
            throw new DomainException("City is required.");

        if (victimsCount < 0)
            throw new DomainException("VictimsCount cannot be negative.");

        var list = vehicles?.ToList() ?? throw new DomainException("Vehicles collection is required.");
        if (list.Count == 0)
            throw new DomainException("At least one vehicle is required.");

        var entity = new Accident
        {
            Id = AccidentId.New(),
            OccurredAt = occurredAt,
            Department = department.Trim(),
            City = city.Trim(),
            Type = type,
            VictimsCount = victimsCount,
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim()
        };

        entity._vehicles.AddRange(list);
        return entity;
    }
}
