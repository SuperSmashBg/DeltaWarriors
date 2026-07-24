using BaseLib.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Behaviors;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Keywords;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Uncommon;

public class DeliberateMisinterpretationCage() : CageCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self), IOnScry
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ScryVar(2).WithTooltip(), new EnergyVar(1)];
    // public override IEnumerable<CardKeyword> CanonicalKeywords => [DeltaKeywords.Manual];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(DeltaEnums.ToBalance),
        HoverTipFactory.Static(DeltaEnums.ToExpand)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await ScryBehavior.Execute(choiceContext, Owner, DynamicVars.ScryVar().IntValue, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.ScryVar().UpgradeValueBy(1);
    }

    public Task OnScry(PlayerChoiceContext choiceContext, Player player, IEnumerable<CardModel> discarded, int scryTotal, AbstractModel? source)
    {
        try
        {
            if (source == null || source != this || player != Owner ) return Task.CompletedTask;
            foreach (CardModel cardModel in discarded)
            {
                cardModel.EnergyCost.AddThisCombat(-DynamicVars.Energy.IntValue);
            }
            
            return Task.CompletedTask;
        }
        catch (Exception exception)
        {
            return Task.FromException(exception);
        }
    }
}