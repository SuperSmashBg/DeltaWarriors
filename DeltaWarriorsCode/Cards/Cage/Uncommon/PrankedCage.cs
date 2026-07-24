using BaseLib.Extensions;
using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using DeltaWarriors.DeltaWarriorsCode.Powers.Temp;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Powers.Mocks;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Uncommon;


public class PrankedCage() : CageCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    // This seems like a more proper way to do this
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TempPrankedPower>("StrengthLoss", 4), new PowerVar<WeakPower>(1)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<StrengthPower>(DynamicVars["StrengthLoss"].IntValue)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (play.Target != null)
        {
            await CommonActions.Apply<WeakPower>(choiceContext, play.Target, this);
            await PowerCmd.Apply<TempPrankedPower>(choiceContext, play.Target, DynamicVars["StrengthLoss"].BaseValue, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["StrengthLoss"].UpgradeValueBy(2);
        DynamicVars.Power<WeakPower>().UpgradeValueBy(1);
    }

    
}