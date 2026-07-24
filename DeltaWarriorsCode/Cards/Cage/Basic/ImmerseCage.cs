using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Behaviors;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Keywords;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using DeltaWarriors.DeltaWarriorsCode.Powers.Temp;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Basic;

public class ImmerseCage() : CageCard(0,
    CardType.Skill, CardRarity.Basic,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ScryVarOld(2).WithTooltip(), 
        new PowerVar<OtherworldlyCorruptionPower>(2)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(DeltaEnums.ToExpand), HoverTipFactory.Static(DeltaEnums.ToBalance),
        HoverTipFactory.FromPower<OtherworldlyCorruptionPower>(DynamicVars.Power<OtherworldlyCorruptionPower>().IntValue)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.Apply<OtherworldlyCorruptionPower>(choiceContext, this, play);
        await ScryBehavior.Execute(choiceContext, Owner, DynamicVars.ScryVarOld().IntValue, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.ScryVarOld().UpgradeValueBy(1);
    }
    
}