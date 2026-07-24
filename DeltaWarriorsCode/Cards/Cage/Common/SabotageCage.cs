using BaseLib.Extensions;
using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Extensions;
using DeltaWarriors.DeltaWarriorsCode.Keywords;
using DeltaWarriors.DeltaWarriorsCode.Powers;
using DeltaWarriors.DeltaWarriorsCode.Powers.Temp;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Common;

public class SabotageCage() : CageCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(8, ValueProp.Move), 
        new PowerVar<TempSabotageCardPower>(1)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        if (!Owner.HasPower<CommandPower>())
            await CommonActions.ApplySelf<TempSabotageCardPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3);
    }
}