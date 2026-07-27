using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Rare;

public class MissingHandCage() : CageCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<StrengthPower>(2), 
        new PowerVar<DexterityPower>(2), 
        new PowerVar<MissingHandPower>(1), 
        new CardsVar(7)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<StrengthPower>(DynamicVars.Strength.IntValue),
        HoverTipFactory.FromPower<DexterityPower>(DynamicVars.Dexterity.IntValue),
        HoverTipFactory.Static(DeltaEnums.ToExpand)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<MissingHandPower>(choiceContext, this);
        await CommonActions.ApplySelf<StrengthPower>(choiceContext, this);
        await CommonActions.ApplySelf<DexterityPower>(choiceContext, this);
        // Remove extra cards in hand to enforce new hand size
        await CardPileCmd.Add(CardPile.GetCards(Owner, PileType.Hand).Skip(DynamicVars.Cards.IntValue), PileType.Discard, CardPilePosition.Top);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Strength.UpgradeValueBy(1);
        DynamicVars.Dexterity.UpgradeValueBy(1);
    }
}