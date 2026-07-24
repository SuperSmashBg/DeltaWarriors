using BaseLib.Hooks;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;

public class MissingHandPower() : DeltaWarriorsPower, IMaxHandSizeModifier
{
    public override PowerType Type =>
        PowerType.Debuff;

    public override PowerStackType StackType =>
        PowerStackType.Single;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(7)];

    public int ModifyMaxHandSizeLate(Player player, int currentMaxHandSize) 
        => player == Owner.Player ? DynamicVars.Cards.IntValue : currentMaxHandSize;
}