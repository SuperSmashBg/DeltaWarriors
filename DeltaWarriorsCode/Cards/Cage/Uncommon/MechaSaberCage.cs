using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Uncommon;

public class MechaSaberCage() : CageCard(1,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move), new DynamicVar("Increase", 4)];
    private Decimal _extraDamageFromPlays;
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (Owner.PlayerCombatState == null) return;
        if (CombatState != null) await CommonActions.CardAttack(this, play).Execute(choiceContext);
        IEnumerable<MechaSaberCage> likeCards = Owner.PlayerCombatState.AllCards.OfType<MechaSaberCage>();
        foreach (MechaSaberCage card in likeCards) card.BuffFromLikePlay(DynamicVars["Increase"].BaseValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Increase"].UpgradeValueBy(2);
        DynamicVars.Damage.UpgradeValueBy(2);
    }
    
    protected override void AfterDowngraded()
    {
        base.AfterDowngraded();
        DynamicVars.Damage.BaseValue += this._extraDamageFromPlays;
    }

    private void BuffFromLikePlay(Decimal extraDamage)
    {
        DynamicVars.Damage.BaseValue += DynamicVars["Increase"].BaseValue;
        _extraDamageFromPlays += DynamicVars["Increase"].BaseValue;
    }
}