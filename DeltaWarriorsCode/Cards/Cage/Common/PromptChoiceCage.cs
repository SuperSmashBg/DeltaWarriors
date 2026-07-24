using BaseLib.Extensions;
using BaseLib.Utils;
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

public class PromptChoiceCage() : CageCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.AnyEnemy) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<DiminishPower>(3),
        new PowerVar<TempPromptChoicePower>(1)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<CommandPower>(),
        HoverTipFactory.FromPower<DiminishPower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.Apply<DiminishPower>(choiceContext, this, play);
        await CommonActions.ApplySelf<TempPromptChoicePower>(choiceContext, this);
    }

    protected override void OnUpgrade() {
        DynamicVars.Power<DiminishPower>().UpgradeValueBy(3);
    }
}