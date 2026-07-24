using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Keywords;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Rare;

public class SealTheFountainCage() : CageCard(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CalculationBaseVar(0), new CalculationExtraVar(1),
    new CalculatedBlockVar(ValueProp.Move).WithMultiplier((c, _) => c.Owner.Deck.Cards.Count)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [DeltaKeywords.Manual];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(DeltaEnums.ToBalance)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, DynamicVars.CalculatedBlock, play);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(DeltaKeywords.Manual);
    }
}