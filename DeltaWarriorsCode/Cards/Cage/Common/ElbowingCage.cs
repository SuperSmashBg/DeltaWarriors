using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Common;

// TODO
//  This card SUCKS fix it
public class ElbowingCage() : CageCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(6),
        new ExtraDamageVar(8),
        // Cannot use _wasPlayedAutomatically like this!!!
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((model, _) => (model as ElbowingCage)?._wasPlayedAutomatically ?? false ? 1 : 0)];

    private bool _wasPlayedAutomatically = false;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(DeltaEnums.ToBalance)];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target, DynamicVars.CalculatedDamage,
            vfx: "vfx/vfx_attack_slash").Execute(choiceContext);
        _wasPlayedAutomatically = false;
        
    }

    protected override void OnUpgrade()
    {
        DynamicVars.ExtraDamage.UpgradeValueBy(5);
    }
    
    public override Task BeforeCardAutoPlayed(CardModel card, Creature? target, AutoPlayType type)
    {
        if (card == this)_wasPlayedAutomatically = true;
        return base.BeforeCardAutoPlayed(card, target, type);
    }
}