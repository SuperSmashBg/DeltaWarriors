using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;

 
public class DistortedReflectionPower() : DeltaWarriorsPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player)
            return Task.CompletedTask;
        _amountsForPlayedCards.Add(cardPlay.Card, cardPlay.Card.EnergyCost.HasLocalModifiers ? Amount : -1);
        
        return Task.CompletedTask;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // Filter like this cause serpent form does and says its important idk
        if (cardPlay.Card.Owner != Owner.Player || !_amountsForPlayedCards.Remove(cardPlay.Card, out var damage) || damage <= 0) return;
        // Filter for modified card cost
        if (!cardPlay.Card.EnergyCost.HasLocalModifiers) return;
        if (Owner.CombatState == null) return;
        Flash();
        await CreatureCmd.Damage(choiceContext, Owner.CombatState.HittableEnemies, Amount, ValueProp.Unpowered, Owner);
    }

    /// <summary>
    /// Keep track of the cards we've seen played and the power amount at the time they were played.
    /// This lets Serpent Form avoid triggering on cards that started play before it was applied, and avoid
    /// dealing extra damage on multiple plays of Serpent Form.
    /// </summary>
    private readonly Dictionary<CardModel, int> _amountsForPlayedCards = new ();
}