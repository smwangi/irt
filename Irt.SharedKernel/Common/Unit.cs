namespace Irt.SharedKernel.Common;

public sealed record Unit
{
    public static readonly Unit Value = new Unit();

    private Unit() { }

    public override string ToString() => "Unit";
}