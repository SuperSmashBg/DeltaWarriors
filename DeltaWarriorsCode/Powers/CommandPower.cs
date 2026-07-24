using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.TestSupport;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;

public class CommandPower() : DeltaWarriorsPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterAutoPostPlayPhaseEntered(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player) return;
        if (Owner.Powers.Count(m => m.GetType() == typeof(NoCommandPower)) != 0) return;
        
        // Hopefully this uses Vaku Selector for any decisions
        // Still prob random targeting tho
        // Seems to work fine
        using (CardSelectCmd.PushSelector(new VakuuCardSelector()))
        {
            await CardPileCmd.AutoPlayFromDrawPile(choiceContext, player, this.Amount, CardPilePosition.Top, false);
        }
    }
}