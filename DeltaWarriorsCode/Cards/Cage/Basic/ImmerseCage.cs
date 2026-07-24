using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Commands;
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
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ScryVar(2).WithTooltip(), 
        new PowerVar<DiminishPower>(2)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [DeltaKeywords.Manual];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(DeltaEnums.ToExpand), 
        HoverTipFactory.Static(DeltaEnums.ToBalance),
        HoverTipFactory.FromPower<DiminishPower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.Apply<DiminishPower>(choiceContext, this, play);
        await ScryCmd.Execute(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Scry().UpgradeValueBy(1);
        DynamicVars.Power<DiminishPower>().UpgradeValueBy(1);
    }
    
}