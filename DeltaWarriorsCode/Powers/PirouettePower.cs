using DeltaWarriors.DeltaWarriorsCode.Behaviors;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;


public class PirouettePower() : DeltaWarriorsPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(1)];

    public override Task AfterPlayerTurnStartLate(PlayerChoiceContext choiceContext, Player player)
    {
        if (Owner.Player != player) return base.AfterPlayerTurnStart(choiceContext, player);
        for (int count = 0; count < Amount; count++)
        {
            CardModel? handCard = DeltaCmd.SmartSelectForReduction(Owner.Player, PileType.Hand);
            CardModel? drawCard = DeltaCmd.SmartSelectForChange(Owner.Player, PileType.Draw);
            handCard?.EnergyCost.AddThisCombat(-DynamicVars.Energy.IntValue);
            drawCard?.EnergyCost.AddThisCombat(DynamicVars.Energy.IntValue);
        }
        return base.AfterPlayerTurnStartLate(choiceContext, player);
    }
}