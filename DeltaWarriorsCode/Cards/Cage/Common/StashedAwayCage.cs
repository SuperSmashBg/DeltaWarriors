using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Common;

public class StashedAwayCage() : CageCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<RetainHandPower>(1)
    ];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(CardKeyword.Retain)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) =>
        await CommonActions.ApplySelf<RetainHandPower>(choiceContext, this);

    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Exhaust);
}