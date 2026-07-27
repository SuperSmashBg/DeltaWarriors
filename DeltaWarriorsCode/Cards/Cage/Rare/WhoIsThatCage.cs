using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Keywords;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rewards;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Rare;

public class WhoIsThatCage() : CageCard(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(3)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust, DeltaKeywords.Manual];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        List<CardPoolModel> unlockedPools = Owner.UnlockState.CharacterCardPools.ToList();
        List<CardModel> unlockedCards = unlockedPools.SelectMany(charCards => charCards.GetUnlockedCards(Owner.UnlockState, Owner.RunState.CardMultiplayerConstraint)).ToList();
        List<CardModel> cardOptions = CardFactory.GetDistinctForCombat(Owner, unlockedCards, DynamicVars.Cards.IntValue,
            Owner.RunState.Rng.CombatCardGeneration).ToList();
        CardModel? chosenCard;
        if (DynamicVars.Cards.IntValue > 3)
        {
            chosenCard = (await CardSelectCmd
                    .FromSimpleGrid(choiceContext, cardOptions, Owner,
                        new CardSelectorPrefs(DeltaWarriorSelectorPerfs.WhoIsThatSelectionPrompt, 1)))
                .FirstOrDefault();
        }
        else
        {
            chosenCard = await CardSelectCmd.FromChooseACardScreen(choiceContext, cardOptions, Owner);
        }
        
        chosenCard?.SetToFreeThisCombat();
        if (chosenCard != null) await CardPileCmd.Add(chosenCard, PileType.Hand);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(2);
    }
}