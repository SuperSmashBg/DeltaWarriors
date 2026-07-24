using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Behaviors;

public class DeltaHooks
{
    
    private static async Task Dispatch<T>(
        PlayerChoiceContext choiceContext,
        Player player,
        Func<AbstractModel, Task> invoke) where T : class
    {
        ICombatState? combatState = player.Creature.CombatState;
        if (combatState == null) return;
        foreach (var absObj in combatState.IterateHookListeners().Where(x => x is T))
        {
            choiceContext.PushModel(absObj);
            await invoke(absObj);
            choiceContext.PopModel(absObj);
        }
    }

    public static Task OnScry(
        PlayerChoiceContext choiceContext,
        Player player,
        IEnumerable<CardModel> discarded,
        int scryTotal,
        AbstractModel source) => Dispatch<IOnScry>(choiceContext, player,
            model => ((IOnScry)model).OnScry(choiceContext, player, discarded, scryTotal, source));
}