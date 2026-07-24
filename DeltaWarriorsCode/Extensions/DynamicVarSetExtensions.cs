using DeltaWarriors.DeltaWarriorsCode.Keywords;
using DeltaWarriors.DeltaWarriorsCode.Powers.Temp;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DeltaWarriors.DeltaWarriorsCode.Extensions;

public static class DynamicVarSetExtensions
{
    public static ScryVar ScryVar(this DynamicVarSet vars)
    {
        return (ScryVar)vars["dwScry"];
    }
    
    public static DynamicVar TempCommandVar(this DynamicVarSet vars)
    {
        return vars["dwTempCommand"];
    }
}