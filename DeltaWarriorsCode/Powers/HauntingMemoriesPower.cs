using DeltaWarriors.DeltaWarriorsCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Powers;

public class HauntingMemoriesPower() : DeltaWarriorsPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterShuffle(PlayerChoiceContext choiceContext, Player shuffler)
    {
        if (Owner.Player == null || shuffler != Owner.Player) return;
        IReadOnlyList<Creature> hittableEnemies = CombatState.HittableEnemies;
        if (hittableEnemies.Count == 0) return;
        Creature target = Owner.Player.RunState.Rng.CombatTargets.NextItem(hittableEnemies) ?? throw new InvalidOperationException();
        Flash();
        await CreatureCmd.Damage(choiceContext, target, Amount, ValueProp.Unpowered, null, null);
    }
}