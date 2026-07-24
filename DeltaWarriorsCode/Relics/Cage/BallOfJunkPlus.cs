using BaseLib.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Keywords;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Relics.Cage;


public class BallOfJunkPlus() : DeltaWarriors.DeltaWarriorsCode.Relics.CageRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    private CardModel? _selectedCard;
    private bool _usedThisCombat = true;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new RepeatVar(2)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(DeltaEnums.ToRework)];

    public override Task BeforeCombatStart()
    {
        _usedThisCombat = false;
        _selectedCard = null;
        Status = RelicStatus.Active;
        return base.BeforeCombatStart();
    }

    public override Task BeforeCardAutoPlayed(CardModel card, Creature? target, AutoPlayType type)
    {
        if (_selectedCard != null || card.Owner != Owner || card.Type == CardType.Power || card.Keywords.Contains(CardKeyword.Exhaust) 
            || card.Keywords.Contains(DeltaKeywords.Manual) || card.Keywords.Contains(CardKeyword.Unplayable)) return base.BeforeCardAutoPlayed(card, target, type);
        _selectedCard = card;
        Flash();
        return base.BeforeCardAutoPlayed(card, target, type);
        
    }

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
    {
        if (player != Owner || _usedThisCombat || _selectedCard == null) return;
        if (_selectedCard.Pile != null && _selectedCard.Pile.Type != PileType.Exhaust && _selectedCard.Pile.Type != PileType.None)
        {
            await CardPileCmd.Add(_selectedCard, PileType.Hand);
            _selectedCard.EnergyCost.SetThisCombat(0);
            _selectedCard.BaseReplayCount += DynamicVars.Repeat.IntValue;
            Flash();
        }
        _usedThisCombat = true;
       
        Status = RelicStatus.Normal;
    }
}