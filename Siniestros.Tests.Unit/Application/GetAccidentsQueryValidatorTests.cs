using FluentAssertions;
using Siniestros.Application.Accidents.Search;
using Xunit;

namespace Siniestros.Tests.Unit.Application;

public class GetAccidentsQueryValidatorTests
{
    [Fact]
    public void Validator_ShouldFail_WhenFromGreaterThanTo()
    {
        var validator = new GetAccidentsQueryValidator();

        var query = new GetAccidentsQuery(
            Department: null,
            From: DateTimeOffset.Parse("2026-01-15T00:00:00-05:00"),
            To: DateTimeOffset.Parse("2026-01-10T00:00:00-05:00"),
            PageNumber: 1,
            PageSize: 20);

        var result = validator.Validate(query);

        result.IsValid.Should().BeFalse();
    }
}
