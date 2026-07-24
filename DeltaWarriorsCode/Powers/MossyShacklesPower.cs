using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;

  
public class MossyShacklesPower() : DeltaWarriorsPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task BeforeCardAutoPlayed(CardModel card, Creature? target, AutoPlayType type)
    {
        if (card.Owner.Creature == Owner)
        {
            await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null); 
        }
        await base.BeforeCardAutoPlayed(card, target, type);
    }
}