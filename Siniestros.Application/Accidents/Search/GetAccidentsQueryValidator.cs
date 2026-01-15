using FluentValidation;

namespace Siniestros.Application.Accidents.Search;

public sealed class GetAccidentsQueryValidator : AbstractValidator<GetAccidentsQuery>
{
    public GetAccidentsQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);

        RuleFor(x => x)
            .Must(x => !(x.From.HasValue && x.To.HasValue && x.From > x.To))
            .WithMessage("From cannot be greater than To.");
    }
}
