using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Ancient;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Keywords;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using DeltaWarriors.DeltaWarriorsCode.Powers.Temp;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Basic;


public class ActCage() : CageCard(1,
    CardType.Skill, CardRarity.Basic,
    TargetType.Self), ITranscendenceCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new TempCommandVar(1).WithTooltip()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<TempActCardPower>(choiceContext, this, DynamicVars.TempCommandVar().BaseValue);
    }

    protected override void OnUpgrade()
    {
        //this.DynamicVars["dwTempCommand"].UpgradeValueBy(1); Seemed too OP
        EnergyCost.UpgradeBy(-1);
    }

    public CardModel GetTranscendenceTransformedCard() => ModelDb.Card<XActionCage>();
}