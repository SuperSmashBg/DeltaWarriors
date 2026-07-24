using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Powers.Temp;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DeltaWarriors.DeltaWarriorsCode.Keywords;

public class TempCommandVar : DynamicVar
{
    public const string Key = "dwTempCommand";
    
    public TempCommandVar(decimal tempCommandCount) : base(Key, tempCommandCount)
    {
        this.WithTooltip();
    }
    
}