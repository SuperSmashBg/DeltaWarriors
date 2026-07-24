using DeltaWarriors.DeltaWarriorsCode.Behaviors;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Rare;

public class BunkerCodesCage() : CageCard(0,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override bool HasEnergyCostX => true;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(1)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int energyXValue = ResolveEnergyXValue() + (IsUpgraded ? 1 : 0);
        for (var i = 0; i < energyXValue; i++)
        {
            CardModel? card = DeltaCmd.SmartSelectForReduction(Owner, PileType.Hand);
            if (card == null) break;
            card.EnergyCost.AddThisCombat(-DynamicVars.Energy.IntValue);
        }
        return base.OnPlay(choiceContext, play);
    }

    protected override void OnUpgrade()
    {

    }
}