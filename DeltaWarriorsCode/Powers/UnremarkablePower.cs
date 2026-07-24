using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;

public class UnremarkablePower() : DeltaWarriorsPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.Player == null
            || cardPlay.Card.Owner != Owner.Player
            || (cardPlay.Card.Rarity != CardRarity.Basic
                && cardPlay.Card.Rarity != CardRarity.Common)) return;
        await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null); 
    }
}