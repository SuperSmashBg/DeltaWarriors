using BaseLib.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Keywords;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Behaviors;

// Adappted from Watcher Mod
public static class ScryBehavior
{
    public static async Task Execute(PlayerChoiceContext choiceContext, Player player, int amount, AbstractModel source)
    {
        
        // Do nothing for scry <1
        if (amount < 1) {return;}
        
        // Get the cards off of the draw pile
        List<CardModel> drawPileCards = PileType.Draw.GetPile(player).Cards.Take(amount).ToList();
        
        // Do nothing if no cards
        if (drawPileCards.Count == 0) {return;}
        
        // Looks like it sets the selector to use aka discard 2
        CardSelectorPrefs prefs =
            new CardSelectorPrefs(DeltaWarriorSelectorPerfs.ScrySelectionPrompt, 0, drawPileCards.Count);

        // Make a list of selected cards to discard
        List<CardModel> discardList = (await CardSelectCmd.FromSimpleGrid(choiceContext, drawPileCards, player, prefs)).ToList();

        // Discard the cards
        await CardCmd.Discard(choiceContext, discardList);
        
        await DeltaHooks.OnScry(choiceContext, player, discardList, drawPileCards.Count, source);
    }

    public static Task Execute(PlayerChoiceContext choiceContext, Player player, AbstractModel source)
    {
        int amount = source.GetDynamicVar(ScryVarOld.Key).IntValue;
        return Execute(choiceContext, player, amount, source);
    }
    
}