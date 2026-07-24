using DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Common;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Uncommon;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Powers.Temp;

public class TempSaberTenPower() : TempCommandPower
{
    public override AbstractModel OriginModel => ModelDb.Card<SaberTenCage>();
}