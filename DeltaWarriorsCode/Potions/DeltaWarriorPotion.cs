using BaseLib.Abstracts;
using BaseLib.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Potions;

namespace DeltaWarriors.DeltaWarriorsCode.Potions;

public abstract class DeltaWarriorPotion(PotionRarity rarity, TargetType target, PotionUsage usage) :
    CustomPotionModel
{
    public override PotionRarity Rarity => rarity;
    public override TargetType TargetType => target;
    public override PotionUsage Usage => usage;
    
}