using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;
  
public class NoDamagePower() : DeltaWarriorsPower
{
    public override PowerType Type =>
        PowerType.Debuff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    // Set all HP loss to 0 caused by owner
    public override decimal ModifyHpLostBeforeOstyLate(Creature target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource)
    {
        if (dealer != null && target.IsEnemy && Owner == dealer) return 0;
        return base.ModifyHpLostBeforeOstyLate(target, amount, props, dealer, cardSource);
    }

    public override Task AfterModifyingHpLostBeforeOsty()
    {
        Flash();
        return base.AfterModifyingHpLostBeforeOsty();
    }

    public override async Task BeforeSideTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner)) return;
        if (Owner.Side == CombatSide.Enemy) await PowerCmd.ModifyAmount(new ThrowingPlayerChoiceContext(), this, -1, null, null);
        else await PowerCmd.Decrement(this);
    }

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource, CardPlay? cardPlay)
    {
        if (dealer != Owner) return base.ModifyDamageMultiplicative(target, amount, props, dealer, cardSource, cardPlay);
        return 0;
    }
}