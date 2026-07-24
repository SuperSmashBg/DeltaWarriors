using BaseLib.Abstracts;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Random;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;

public class ShrinePower() : DeltaWarriorsPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(3)];

    public override async Task AfterAutoPrePlayPhaseEnteredLate(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player || Owner.CombatState == null) return;
        using (CardSelectCmd.PushSelector(new VakuuCardSelector()))
        {
            int plays = 0;
            while (!CombatManager.Instance.IsOverOrEnding && !CombatManager.Instance.IsPlayerReadyToEndTurn(player) && plays < 25)
            {
                CardModel? selectedCard = PileType.Hand.GetPile(Owner.Player).Cards.FirstOrDefault(c => c.CanPlay());
                if (selectedCard == null) break;
                Creature? target = GetTarget(selectedCard, Owner.CombatState);
                await selectedCard.SpendResources();
                await CardCmd.AutoPlay(choiceContext, selectedCard, target, skipXCapture: true);
                plays++;
            }
        }
        await PowerCmd.ModifyAmount(new ThrowingPlayerChoiceContext(), this, -1, null, null);
        
    }

    public override decimal ModifyMaxEnergy(Player player, decimal amount)
    {
        return player == Owner.Player ? amount + DynamicVars.Energy.IntValue : amount;
    }
    
    private Creature? GetTarget(CardModel card, ICombatState combatState)
    {
        if (Owner.Player == null) return null;
        Rng combatTargets = Owner.Player.RunState.Rng.CombatTargets;
        Creature? target = card.TargetType switch
        {
            TargetType.AnyEnemy => combatState.HittableEnemies.Count > 0 ? CombatState.HittableEnemies[0] : null,
            TargetType.AnyPlayer => Owner,
            TargetType.AnyAlly => combatTargets.NextItem(combatState.Allies.Where(c =>
                c is { IsAlive: true, IsPlayer: true } && c != Owner)),
            _ => null
        };
        return target;
    }
}