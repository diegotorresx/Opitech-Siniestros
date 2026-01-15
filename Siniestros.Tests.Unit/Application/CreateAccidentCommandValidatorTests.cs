using FluentAssertions;
using Siniestros.Application.Accidents.Create;
using Siniestros.Domain.Accidents;
using Xunit;

namespace Siniestros.Tests.Unit.Application;

public class CreateAccidentCommandValidatorTests
{
    [Fact]
    public void Validator_ShouldFail_WhenVehiclesEmpty()
    {
        var validator = new CreateAccidentCommandValidator();

        var cmd = new CreateAccidentCommand(
            DateTimeOffset.Now,
            "Córdoba",
            "Planeta Rica",
            AccidentType.Collision,
            new List<VehicleRequest>(),
            0,
            null);

        var result = validator.Validate(cmd);

        result.IsValid.Should().BeFalse();
    }
}
