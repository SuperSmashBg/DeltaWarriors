using DeltaWarriors.DeltaWarriorsCode.Potions.Cage;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Powers.Temp;

public class TempHollowPotionPower() : TempCommandPower
{
    public override AbstractModel OriginModel => ModelDb.Potion<HollowFlaskPotion>();
}