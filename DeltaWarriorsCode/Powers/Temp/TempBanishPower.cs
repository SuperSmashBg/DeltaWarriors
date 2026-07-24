using DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Rare;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Powers.Temp;

public class TempBanishPower() : TempCommandPower
{
    public override AbstractModel OriginModel => ModelDb.Card<BanishCage>();
}