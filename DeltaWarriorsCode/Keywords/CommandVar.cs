using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DeltaWarriors.DeltaWarriorsCode.Keywords;

[Obsolete("Use Hovertip Factory")]
public class CommandVar: DynamicVar
{
    public const string Key = "dwCommand";
    
    public CommandVar(decimal tempCommandCount) : base(Key, tempCommandCount)
    {
        this.WithTooltip();
    }
    
}
