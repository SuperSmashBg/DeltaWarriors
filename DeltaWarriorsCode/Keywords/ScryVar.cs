using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DeltaWarriors.DeltaWarriorsCode.Keywords;

public class ScryVar : DynamicVar
{
    public const string Key = "dwScry";
    
    public ScryVar(decimal scryCount)  : base(Key, scryCount)
    {
        this.WithTooltip();
    }
    
}