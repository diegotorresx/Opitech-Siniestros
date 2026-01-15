using FluentValidation;

namespace Siniestros.Application.Accidents.Create;

public sealed class CreateAccidentCommandValidator : AbstractValidator<CreateAccidentCommand>
{
    public CreateAccidentCommandValidator()
    {
        RuleFor(x => x.OccurredAt).NotEmpty();
        RuleFor(x => x.Department).NotEmpty().MaximumLength(100);
        RuleFor(x => x.City).NotEmpty().MaximumLength(100);
        RuleFor(x => x.VictimsCount).GreaterThanOrEqualTo(0);

        RuleFor(x => x.Vehicles)
            .NotNull()
            .Must(v => v.Count > 0)
            .WithMessage("At least one vehicle is required.");

        RuleForEach(x => x.Vehicles).ChildRules(v =>
        {
            v.RuleFor(a => a.Type).NotEmpty().MaximumLength(50);
            v.RuleFor(a => a.Plate).MaximumLength(20);
        });

        RuleFor(x => x.Description).MaximumLength(500);
    }
}
