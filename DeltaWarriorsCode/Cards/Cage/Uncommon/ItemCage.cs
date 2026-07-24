using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Uncommon;

public class ItemCage() : CageCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        // Make a card using a card factory
        var genCard = CardFactory.GetDistinctForCombat(Owner,
                Owner.Character.CardPool //Grab our card pool
                    .GetUnlockedCards(Owner.UnlockState, Owner.RunState.CardMultiplayerConstraint) // Get unlocked/available cards
                    .Where(c => c.Type == CardType.Skill), 1, // One Skill
                Owner.RunState.Rng.CombatCardGeneration)
            .FirstOrDefault();

        if (genCard == null) { return; }

        if (IsUpgraded)
        {
            genCard.SetToFreeThisCombat();
        }
        else
        {
            genCard.SetToFreeThisTurn();
        }
        
        CardCmd.ApplyKeyword(genCard, CardKeyword.Exhaust);
        await CardPileCmd.AddGeneratedCardToCombat(genCard, PileType.Hand, Owner);

    }

    protected override void OnUpgrade()
    {
    }
}