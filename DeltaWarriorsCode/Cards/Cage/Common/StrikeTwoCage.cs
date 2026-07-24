using BaseLib.Extensions;
using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Common;

public class StrikeTwoCage() : CageCard(2,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(15, ValueProp.Move),
        new PowerVar<DiminishPower>(4)
    ];
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<DiminishPower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        await CommonActions.Apply<DiminishPower>(choiceContext, this, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4);
        DynamicVars.Power<DiminishPower>().UpgradeValueBy(2);
    }
}