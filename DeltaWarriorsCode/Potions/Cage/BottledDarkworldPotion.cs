using BaseLib.Abstracts;
using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Potions.Cage;

[Pool(typeof(TheCagePotionPool))]
public class BottledDarkworldPotion : CustomPotionModel
{
    public override PotionRarity Rarity => PotionRarity.Rare;
    public override TargetType TargetType => TargetType.Self;
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(1)];

    protected override Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        if (target is not { Player: not null }) return base.OnUse(choiceContext, target);
        IEnumerable<CardModel> hand = PileType.Hand.GetPile(target.Player).Cards;
        foreach (var card in hand)
        {
            card.EnergyCost.AddThisCombat(-DynamicVars.Energy.IntValue);
        }
        return base.OnUse(choiceContext, target);
    }
}