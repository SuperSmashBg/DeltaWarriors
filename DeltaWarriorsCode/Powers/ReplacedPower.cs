using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Keywords;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;

public class ReplacedPower : DeltaWarriorsPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    private bool _isActive = true;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.Player == null
            || !_isActive
            || Owner.Player != cardPlay.Card.Owner
            || cardPlay.IsAutoPlay
            || !cardPlay.Card.Keywords.Contains(DeltaKeywords.Manual)) return;
        _isActive = false;
        Flash();
        await CardPileCmd.Draw(choiceContext, Amount, Owner.Player);
    }

    public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        if (participants.Contains(Owner)) _isActive = true;
        return base.BeforeSideTurnStart(choiceContext, side, participants, combatState);
    }
}