using DeltaWarriors.DeltaWarriorsCode.Keywords;
using DeltaWarriors.DeltaWarriorsCode.Powers.Temp;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DeltaWarriors.DeltaWarriorsCode.Extensions;

public static class DynamicVarSetExtensions
{
    [Obsolete]
    public static ScryVarOld ScryVarOld(this DynamicVarSet vars)
    {
        return (ScryVarOld)vars["dwScry"];
    }
    
    [Obsolete]
    public static DynamicVar TempCommandVar(this DynamicVarSet vars)
    {
        return vars["dwTempCommand"];
    }
}