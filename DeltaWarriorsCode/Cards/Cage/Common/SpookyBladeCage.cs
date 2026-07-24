using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Common;

  
public class SpookyBladeCage() : CageCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(11, ValueProp.Move), 
        new PowerVar<DrawLessNextTurn>(1)
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(DeltaEnums.ToExpand)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState == null) return;
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        await CommonActions.ApplySelf<DrawLessNextTurn>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4);
    }
}