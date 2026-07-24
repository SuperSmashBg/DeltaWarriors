using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Character;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Keywords;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using DeltaWarriors.DeltaWarriorsCode.Powers.Temp;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DeltaWarriors.DeltaWarriorsCode.Potions.Cage;

[Pool(typeof(TheCagePotionPool))]
public class HollowFlaskPotion: CustomPotionModel
{
    public override PotionRarity Rarity => PotionRarity.Uncommon;
    public override TargetType TargetType => TargetType.AnyPlayer;
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new TempCommandVar(3).WithTooltip()];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        if (target != null) await CommonActions.Apply<TempHollowPotionPower>(choiceContext, target, null, DynamicVars.TempCommandVar().BaseValue);
    }
}