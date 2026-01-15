using FluentAssertions;
using Moq;
using Siniestros.Application.Accidents.Abstractions;
using Siniestros.Application.Accidents.Create;
using Siniestros.Domain.Accidents;
using Xunit;

namespace Siniestros.Tests.Unit.Application;

public class CreateAccidentCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldAddAccident_AndReturnId()
    {
        var repo = new Mock<IAccidentRepository>();
        repo.Setup(r => r.AddAsync(It.IsAny<Accident>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateAccidentCommandHandler(repo.Object);

        var cmd = new CreateAccidentCommand(
            DateTimeOffset.Parse("2026-01-14T09:40:00-05:00"),
            "Córdoba",
            "Planeta Rica",
            AccidentType.Collision,
            new List<VehicleRequest> { new("Car", "AAA111") },
            0,
            "Test");

        var id = await handler.Handle(cmd, CancellationToken.None);

        id.Should().NotBe(Guid.Empty);
        repo.Verify(r => r.AddAsync(It.IsAny<Accident>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
