using DeltaWarriors.DeltaWarriorsCode.Keywords;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;

public class PreparationPower() : DeltaWarriorsPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card.Owner != Owner.Player || card.Type != CardType.Power 
                                       || card.Keywords.Contains(DeltaKeywords.Manual) 
                                       || card.Keywords.Contains(CardKeyword.Unplayable)) return;
        await PowerCmd.ModifyAmount(choiceContext, this, -1, Owner, null);
        await CardCmd.AutoPlay(choiceContext, card, Owner);
    }
}