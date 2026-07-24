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

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Common;

 
public class LightWithin() : CageCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ScryVar(3).WithTooltip(), 
        new PowerVar<TempLightWithinPower>(1)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<CommandPower>()
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        DeltaKeywords.Manual
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await ScryCmd.Execute(choiceContext, this);
        await CommonActions.ApplySelf<TempLightWithinPower>(choiceContext, this);
    }

    protected override void OnUpgrade() {
        DynamicVars.Scry().UpgradeValueBy(2);
    }
}