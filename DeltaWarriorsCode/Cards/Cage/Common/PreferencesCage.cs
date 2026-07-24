using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Behaviors;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Common;

public class PreferencesCage() : CageCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(6, ValueProp.Move), 
        new EnergyVar(1)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        CardModel? card = DeltaCmd.SmartSelectForReduction(Owner, PileType.Hand);
        card?.EnergyCost.AddThisTurnOrUntilPlayed(-DynamicVars.Energy.IntValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}