using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Localization;

namespace DeltaWarriors.DeltaWarriorsCode.Extensions;

public struct DeltaWarriorSelectorPerfs
{
    private const string CardSelectionTable = "card_selection";
    private const string CardTable = "cards";
    
    [Obsolete("Use Baselib scry")]
    public static LocString ScrySelectionPrompt =>
        new LocString(CardSelectionTable, "TO_SCRY");
    
    public static LocString MoveDrawBottomSelectionPrompt =>
        new LocString(CardSelectionTable, "TO_MOVE_DRAW_BOTTOM");

    public static LocString WhoIsThatSelectionPrompt =>
        new LocString(CardTable, "DELTAWARRIORS-WHO_IS_THAT_CAGE.selectionScreenPrompt");
}