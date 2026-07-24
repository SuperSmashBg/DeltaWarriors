using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Keywords;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Ancient;
  
public class VesselsPurposeCage() : CageCard(1,
    CardType.Power, CardRarity.Ancient,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ScryVar(4).WithTooltip()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Innate];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(DeltaEnums.ToExpand)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<VesselsPurposePower>(choiceContext, this, DynamicVars.Scry().BaseValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Scry().UpgradeValueBy(2);
    }
}