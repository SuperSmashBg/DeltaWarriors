using BaseLib.Extensions;
using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Common;


public class SplinterStrikeCage() : CageCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(8, ValueProp.Move), 
        new PowerVar<VulnerablePower>(1)
    ];
    
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<VulnerablePower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play, vfx: "vfx/vfx_attack_slash").Execute(choiceContext);
        await CommonActions.Apply<VulnerablePower>(choiceContext, this, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<VulnerablePower>().UpgradeValueBy(1);
    }
}