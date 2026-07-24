using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;

public class ShadowMantlePower() : DeltaWarriorsPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterShuffle(PlayerChoiceContext choiceContext, Player shuffler)
    {
        if (shuffler != Owner.Player) return;
        double keepPercentage = Math.Pow(0.5, Amount);
        MainFile.Logger.Info("[ShadowMantlePower] Percentage: " + keepPercentage);
        List<CardModel> badCards = PileType.Draw.GetPile(Owner.Player).Cards.Where(c => c.Type is CardType.Status or CardType.Curse).ToList();
        Owner.Player.RunState.Rng.CombatCardSelection.Shuffle(badCards);
        int skipping = (int)Math.Round(keepPercentage * badCards.Count);
        MainFile.Logger.Info("[ShadowMantlePower] Skipping int: " + skipping);
        IEnumerable<CardModel> moveCards = badCards.Skip(skipping).ToList();
        MainFile.Logger.Info("[ShadowMantlePower] Skipped: " + moveCards.Count());
        await CardPileCmd.Add(moveCards, PileType.Discard);
        return;
    }
}