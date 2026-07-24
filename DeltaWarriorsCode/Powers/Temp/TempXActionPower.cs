using DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Ancient;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Powers.Temp;

  
public class TempXActionPower() : TempCommandPower
{
    public override AbstractModel OriginModel => ModelDb.Card<XActionCage>();
}