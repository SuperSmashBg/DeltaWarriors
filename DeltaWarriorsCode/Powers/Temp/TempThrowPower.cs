using DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Common;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Powers.Temp;

public class TempThrowPower: TempCommandPower
{
    public override AbstractModel OriginModel => ModelDb.Card<ThrowCage>();
}