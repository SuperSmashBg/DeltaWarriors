using DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Basic;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Powers.Temp;

  
public class TempLightWithinPower() : TempCommandPower
{
    public override AbstractModel OriginModel => ModelDb.Card<ActCage>();
}