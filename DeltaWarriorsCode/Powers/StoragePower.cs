using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;


public class StoragePower() : DeltaWarriorsPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Single;

    public override Task AfterCardDrawnEarly(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        // Get the potions used and early card plays for the owner
        IEnumerable<CombatHistoryEntry> relevantEntries = CombatManager.Instance.History.Entries
            .Where(e => e.HappenedThisTurn(CombatState)
                        && e.Actor == Owner
                        && e is PotionUsedEntry or CardPlayStartedEntry)
            .ToList(); 
        MainFile.Logger.Info("[StoragePower] Combat Entries Selected: " + relevantEntries.Count());
        try
        {
            MainFile.Logger.Info("[StoragePower] Top Entry: " + relevantEntries.Last().Description);
            // Disregard if the last entry was not a CardPlayFinished Autoplay
            if (!(relevantEntries.Last() as CardPlayStartedEntry)?.CardPlay.IsAutoPlay ?? true) 
                return base.AfterCardDrawnEarly(choiceContext, card, fromHandDraw);
        }
        catch
        {
            return base.AfterCardDrawnEarly(choiceContext, card, fromHandDraw);
        }
        card.GiveSingleTurnRetain();
        Flash();
        return base.AfterCardDrawnEarly(choiceContext, card, fromHandDraw);
    }
}