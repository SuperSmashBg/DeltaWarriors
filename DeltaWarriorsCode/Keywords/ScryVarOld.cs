using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DeltaWarriors.DeltaWarriorsCode.Keywords;

[Obsolete("Use Baselib Scry")]

public class ScryVarOld : DynamicVar
{
    public const string Key = "dwScry";
    
    public ScryVarOld(decimal scryCount)  : base(Key, scryCount)
    {
        this.WithTooltip();
    }
    
}