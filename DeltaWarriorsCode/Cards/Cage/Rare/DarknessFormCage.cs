using BaseLib.Extensions;
using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Keywords;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Rare;

public class DarknessFormCage() : CageCard(3,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new EnergyVar(1), new PowerVar<CommandPower>(3)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [DeltaKeywords.Manual];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<CommandPower>(DynamicVars.Power<CommandPower>().IntValue),
        HoverTipFactory.Static(StaticHoverTip.Energy)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<RoaringEnergyPower>(choiceContext, this, DynamicVars.Energy.BaseValue);
        await CommonActions.ApplySelf<CommandPower>(choiceContext, this, DynamicVars.Power<CommandPower>().BaseValue);
    }
    
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
    
