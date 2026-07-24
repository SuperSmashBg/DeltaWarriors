using BaseLib.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Uncommon;

public class ForethoughtCage() : CageCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        IEnumerable<CardModel> selected = (await CardSelectCmd.FromHand(choiceContext, Owner, 
            new CardSelectorPrefs(DeltaWarriorSelectorPerfs.MoveDrawBottomSelectionPrompt, 0, DynamicVars.Cards.IntValue),
            null, this)).ToList();
        foreach (CardModel card in selected)
        {
            card.EnergyCost.SetUntilPlayed(0);
        }
        await CardPileCmd.Add(selected, PileType.Draw);
        
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(9999);
    }
}