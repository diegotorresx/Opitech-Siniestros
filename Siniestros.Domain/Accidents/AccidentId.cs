namespace Siniestros.Domain.Accidents;

public readonly record struct AccidentId(Guid Value)
{
    public static AccidentId New() => new(Guid.NewGuid());
}
