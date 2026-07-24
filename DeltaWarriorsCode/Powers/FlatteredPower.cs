using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.Runs;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;

public class FlatteredPower : DeltaWarriorsPower
{
    private const string ApplierTag = "Applier";
    private static IEnumerable<Type> AllowList => [typeof(WeakPower), typeof(VulnerablePower),
        typeof(FrailPower), typeof(ShrinkPower), typeof(TangledPower), typeof(DisintegrationPower),
        typeof(SlothPower), typeof(WasteAwayPower), typeof(MindRotPower), typeof(GalvanicPower),
        typeof(StranglePower)
    ];
    
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override PowerInstanceType InstanceType => PowerInstanceType.InstancedPerApplier;


    protected override IEnumerable<DynamicVar> CanonicalVars => [new StringVar(ApplierTag)];


    public override Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        if (Applier is not { Player: not null }) return base.AfterApplied(applier, cardSource);
        ((StringVar)DynamicVars[ApplierTag]).StringValue = CombatState.Players.Count <= 1 ? "You" 
            : PlatformUtil.GetPlayerName(RunManager.Instance.NetService.Platform, Applier.Player.NetId);
        return base.AfterApplied(applier, cardSource);
    }

    public override decimal ModifyPowerAmountGivenMultiplicative(PowerModel power, Creature giver, decimal amount, Creature? target,
        CardModel? cardSource)
    {
        if (!AllowList.Any(t => t.IsInstanceOfType(power)) || target != Applier || giver != Owner) 
            return base.ModifyPowerAmountGivenMultiplicative(power, giver, amount, target, cardSource);
        PowerCmd.ModifyAmount(new ThrowingPlayerChoiceContext(), this, -1, null, null);
        return 0;
    }
}