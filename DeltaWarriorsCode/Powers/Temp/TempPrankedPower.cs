using BaseLib.Abstracts;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Uncommon;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DeltaWarriors.DeltaWarriorsCode.Powers.Temp;

public class TempPrankedPower() : TemporaryStrengthPower, ICustomModel
{
    public override AbstractModel OriginModel => ModelDb.Card<PrankedCage>();
    protected override bool IsPositive => false;
}