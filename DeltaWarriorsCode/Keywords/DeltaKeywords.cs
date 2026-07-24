using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace DeltaWarriors.DeltaWarriorsCode.Keywords;

  
public static class DeltaKeywords
{
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)] public static CardKeyword Manual;
}