using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Uncommon;

public class HiddenTalentCage() : CageCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<DexterityPower>(2)];
    

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
            HoverTipFactory.FromPower<DexterityPower>(DynamicVars.Dexterity.IntValue),
            HoverTipFactory.Static(DeltaEnums.ToBalance)
        ];
    protected override bool IsPlayable => false;
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<DexterityPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Dexterity.UpgradeValueBy(2);
    }
}