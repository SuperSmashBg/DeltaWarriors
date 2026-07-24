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
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Common;

 
public class LightWithin() : CageCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ScryVarOld(2).WithTooltip(), 
        new TempCommandVar(2).WithTooltip()];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await ScryBehavior.Execute(choiceContext, Owner, DynamicVars.ScryVarOld().IntValue, this);
        await CommonActions.ApplySelf<TempLightWithinPower>(choiceContext, this,
            DynamicVars.TempCommandVar().BaseValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.ScryVarOld().UpgradeValueBy(2);
    }
}