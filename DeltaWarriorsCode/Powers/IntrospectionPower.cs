using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;

public class IntrospectionPower : DeltaWarriorsPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    private readonly Queue<CardModel> _savedCards = new Queue<CardModel>();

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player) return base.AfterCardPlayed(choiceContext, cardPlay);
        _savedCards.Enqueue(cardPlay.Card);
        if (_savedCards.Count > Amount) _savedCards.Dequeue();
        return base.AfterCardPlayed(choiceContext, cardPlay);
    }

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
    {
        if (player != Owner.Player) return;
        IEnumerable<CardModel> returningCards =
            _savedCards.Where(c => c.Pile is { Type: not PileType.Exhaust and not PileType.None }).ToList();
        if (returningCards.Any()) await CardPileCmd.Add(returningCards, PileType.Hand);
        _savedCards.Clear();
    }
}