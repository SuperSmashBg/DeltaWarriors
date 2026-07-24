using DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Uncommon;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Powers.Temp;

public class TempNaturalLeaderPower() : TempCommandPower
{
    public override AbstractModel OriginModel => ModelDb.Card<NaturalLeaderCage>();
}