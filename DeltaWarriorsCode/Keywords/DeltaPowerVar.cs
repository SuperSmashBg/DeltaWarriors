using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Keywords;

[Obsolete("WTF IS THIS SHIT")]
public class DeltaPowerVar<T> : PowerVar<T>
    where T : PowerModel
{
    public DeltaPowerVar(decimal powerAmount) : base(powerAmount)
    {
    }

    public DeltaPowerVar(string name, decimal powerAmount) : base(name, powerAmount)
    {
    }
}