using DeltaWarriors.DeltaWarriorsCode.Keywords;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;


namespace DeltaWarriors.DeltaWarriorsCode.Patches;

[HarmonyPatch(typeof(AbstractModel), nameof(AbstractModel.ShouldPlay))]
public class ShouldPlayManualKeywordPatch
{
    static bool Prefix(CardModel card, AutoPlayType autoPlayType, ref CardModel __instance, bool __result)
    {
        if (card == __instance && __instance.Keywords.Contains(DeltaKeywords.Manual) && autoPlayType != AutoPlayType.None)
        {
            __result = false;
            return false;
        }

        return true;
    }
    
}