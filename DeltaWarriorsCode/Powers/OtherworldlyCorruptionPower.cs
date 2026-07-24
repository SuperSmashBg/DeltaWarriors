using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;

 
public class OtherworldlyCorruptionPower() : DeltaWarriorsPower
{
    public override PowerType Type =>
        PowerType.Debuff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource,
        CardPlay? cardPlay)
    {
        if (target != Owner || !props.IsPoweredAttack()) 
            return base.ModifyDamageAdditive(target, amount, props, dealer, cardSource, cardPlay);
        MainFile.Logger.Info("[OtherworldlyCorruptionPower] Cardplay attached: " + cardPlay);
        if (cardPlay is not { IsAutoPlay: true }) 
            return base.ModifyDamageAdditive(target, amount, props, dealer, cardSource, cardPlay);
        Flash();
        MainFile.Logger.Info("[OtherworldlyCorruptionPower] Add Damage Hook called on: " + target.Name);
        return base.ModifyDamageAdditive(target, amount, props, dealer, cardSource, cardPlay) + Amount;
    }
}