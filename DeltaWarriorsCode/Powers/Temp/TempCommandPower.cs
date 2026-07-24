using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Powers.Temp;

public abstract class TempCommandPower : CustomTemporaryPowerModel
{
    public override PowerModel InternallyAppliedPower => ModelDb.Power<CommandPower>();

    protected override Func<PlayerChoiceContext, Creature, decimal, Creature?, CardModel?, bool, Task> ApplyPowerFunc =>
        PowerCmd.Apply<CommandPower>;

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override bool UntilEndOfOtherSideTurn => true;

    public override LocString Title
    {
        get
        {
            return OriginModel switch
            {
                CardModel cardModel => cardModel.TitleLocString,
                PotionModel potionModel => potionModel.Title,
                RelicModel relicModel => relicModel.Title,
                _ => throw new InvalidOperationException()
            };
        }
    }

    public override LocString Description => new ("powers",  "DELTAWARRIORS-TEMP_COMMAND_POWER.description");
    protected override string SmartDescriptionLocKey => "DELTAWARRIORS-TEMP_COMMAND_POWER.smartDescription";
    
}