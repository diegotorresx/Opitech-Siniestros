using FluentAssertions;
using Siniestros.Domain.Accidents;
using Siniestros.Domain.Common;
using Xunit;

namespace Siniestros.Tests.Unit.Domain;

public class AccidentTests
{
    [Fact]
    public void Create_ShouldThrow_WhenVictimsNegative()
    {
        var act = () => Accident.Create(
            DateTimeOffset.Now,
            "Córdoba",
            "Montería",
            AccidentType.Collision,
            new[] { VehicleInvolved.Create("Car", "ABC123") },
            -1,
            null);

        act.Should().Throw<DomainException>()
            .WithMessage("*VictimsCount*");
    }

    [Fact]
    public void Create_ShouldThrow_WhenNoVehicles()
    {
        var act = () => Accident.Create(
            DateTimeOffset.Now,
            "Córdoba",
            "Montería",
            AccidentType.Collision,
            Array.Empty<VehicleInvolved>(),
            0,
            null);

        act.Should().Throw<DomainException>()
            .WithMessage("*At least one vehicle*");
    }
}
