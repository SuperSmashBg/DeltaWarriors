using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Behaviors;

public static class DeltaCmd
{
    // Selects a card to reduce the cost of with similar rules to Mummified Hand
    // 1: Select cards that Cost > Zero Currently
    // 2: Select cards that are not unplayable or X
    // 3: Select cards that are not X
    public static CardModel? SmartSelectForReduction(Player player, List<CardModel> cards)
    {
        List<CardModel> selection = cards.Where(c => !c.EnergyCost.CostsX 
                                                     && !c.Keywords.Contains(CardKeyword.Unplayable)
                                                     && c.EnergyCost.GetResolved() > 0).ToList();
        if (!selection.Any()) selection = cards.Where(c => !c.EnergyCost.CostsX 
                                                           && !c.Keywords.Contains(CardKeyword.Unplayable)).ToList();
        if (!selection.Any()) selection = cards.Where(c => !c.EnergyCost.CostsX).ToList();
        return player.RunState.Rng.CombatCardSelection.NextItem(selection);
    }

    public static CardModel? SmartSelectForReduction(Player player, PileType pile)
    {
        List<CardModel> cards = pile.GetPile(player).Cards.ToList();
        return SmartSelectForReduction(player, cards);
    }
    
    // Selects a card to reduce the cost of with similar rules to Mummified Hand
    // 1: Select cards that are not unplayable or X
    // 2: Select cards that are not X
    public static CardModel? SmartSelectForChange(Player player, List<CardModel> cards)
    {
        List<CardModel> selection = cards.Where(c => !c.EnergyCost.CostsX 
                                                           && !c.Keywords.Contains(CardKeyword.Unplayable)).ToList();
        if (!selection.Any()) selection = cards.Where(c => !c.EnergyCost.CostsX).ToList();
        return player.RunState.Rng.CombatCardSelection.NextItem(selection);
    }

    public static CardModel? SmartSelectForChange(Player player, PileType pile)
    {
        List<CardModel> cards = pile.GetPile(player).Cards.ToList();
        return SmartSelectForReduction(player, cards);
    }
}