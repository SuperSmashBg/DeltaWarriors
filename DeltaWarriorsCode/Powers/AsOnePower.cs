using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;

public class AsOnePower : DeltaWarriorsPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(3)];

    public override async Task AfterSideTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side != Owner.Side) return;
        if (CombatManager.Instance.History.Entries
                .OfType<CardPlayFinishedEntry>()
                .Count(e => e.HappenedThisTurn(CombatState) && e.Actor == Owner && e.CardPlay.IsAutoPlay)
            >= DynamicVars.Cards.IntValue)
        {
            await PowerCmd.Apply<EnergyNextTurnPower>(choiceContext, Owner, Amount, Owner, null, true);
            await PowerCmd.Apply<DrawCardsNextTurnPower>(choiceContext, Owner, Amount, Owner, null, true);
        }
    }
}