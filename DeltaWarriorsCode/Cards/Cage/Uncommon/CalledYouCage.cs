using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Uncommon;

  
public class CalledYouCage() : CageCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    private const string Mult = "Multiplier";
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar(Mult, 2)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(DeltaEnums.ToExpand)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState == null) return;
        foreach (Creature foe in CombatState.Enemies.ToList())
        {
            Decimal stacks = foe.GetPowerAmount<OtherworldlyCorruptionPower>();
            if (stacks > 0)
                await CreatureCmd.Damage(choiceContext, foe, stacks * DynamicVars[Mult].BaseValue, ValueProp.Unpowered, this, play);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars[Mult].UpgradeValueBy(1);
    }
}