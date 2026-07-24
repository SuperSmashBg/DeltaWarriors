using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using BaseLib.Utils;
using DeltaWarriors.DeltaWarriorsCode.Cards.Cage;
using DeltaWarriors.DeltaWarriorsCode.Keywords;
using DeltaWarriors.DeltaWarriorsCode.Powers.Temp;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace DeltaWarriors.DeltaWarriorsCode.Cards.Cage.Uncommon;


public class SaberTenCage() : CageCard(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(10, ValueProp.Move), 
        new DynamicVar("dwTempCommandBase",0),
        new DynamicVar("dwTempCommandExtra", 1),
        new DeltaCalculatedVar("dwTempCommand").WithMultiplier((cardModel, _) => Math.Floor((cardModel as SaberTenCage)?._damage / 10 ?? 0)).WithTooltip(),
    ];
    

    private decimal _damage;
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<TempSaberTenPower>(choiceContext, this, 
            (DynamicVars["dwTempCommand"] as CalculatedVar)?.Calculate(null) ?? 0);
        _damage += (await CommonActions.CardAttack(this, play).Execute(choiceContext))
            .Results
            .SelectMany(r => r)
            .Sum(r => r.TotalDamage + r.OverkillDamage);
    }
    
    public override Task BeforeCombatStart()
    {
        _damage = 0;
        return base.BeforeCombatStart();
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}