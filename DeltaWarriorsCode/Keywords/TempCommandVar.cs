using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Powers.Temp;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DeltaWarriors.DeltaWarriorsCode.Keywords;

[Obsolete("Use PowerVar and HovertipFactory")]
public class TempCommandVar : DynamicVar
{
    public const string Key = "dwTempCommand";
    
    public TempCommandVar(decimal tempCommandCount) : base(Key, tempCommandCount)
    {
        this.WithTooltip();
    }
    
}