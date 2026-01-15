using Siniestros.Domain.Common;

namespace Siniestros.Domain.Accidents;

public sealed record VehicleInvolved(string Type, string? Plate)
{
    public static VehicleInvolved Create(string type, string? plate)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new DomainException("Vehicle type is required.");

        var normalizedType = type.Trim();
        var normalizedPlate = string.IsNullOrWhiteSpace(plate) ? null : plate.Trim();

        return new VehicleInvolved(normalizedType, normalizedPlate);
    }
}
