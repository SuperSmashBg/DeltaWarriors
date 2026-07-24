using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Behaviors;

[Obsolete]
public interface IOnScry
{
    Task OnScry(PlayerChoiceContext choiceContext, Player player, IEnumerable<CardModel> discarded, int scryTotal, AbstractModel source);
}