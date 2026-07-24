using System.Reflection;
using System.Reflection.Emit;
using BaseLib.Utils.Patching;
using DeltaWarriors.DeltaWarriorsCode.Keywords;

using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DeltaWarriors.DeltaWarriorsCode.Patches;

[HarmonyPatch(typeof(CardModel), nameof(CardModel.MoveToResultPileWithoutPlaying), MethodType.Async)]
public static class CardModelMoveToResultsPatch
{
    [HarmonyTranspiler]
    private static List<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        MethodInfo getterFunc = typeof(CardModel).GetProperty(nameof(CardModel.Keywords))?.GetGetMethod() ?? throw new InvalidOperationException();
    
        return new InstructionPatcher(instructions)
            .Match(new InstructionMatcher() // The block of IL code I want to match with
                .ldloc_1() // card
                .call(typeof(CardModel), getterFunc.Name) // call card.Keywords getter - got the getter name via reflection
                .ldc_i4_1() // Places 1 (enum for exhaust keyword)
                .callvirt(typeof(CardModel), nameof(CardModel.Keywords.Contains)) // Virtual Call of CardModel.Keyword.Contains
            )
            .Step(-1).GetInstruction(out var virtCall) // Nab that virtual call because no clue how to make a code instruction for it myself
            .Step(1).GetOperand(out var brTo) // Copy where we branch to for my branch
            .InsertBeforeMatch([
                CodeInstruction.LoadLocal(1), // card 
                CodeInstruction.Call(typeof(CardModel), getterFunc.Name), // Make a call to card.Keywords getter
                CodeInstruction.LoadField(typeof(DeltaKeywords), nameof(DeltaKeywords.Manual)), // Put my custom keyword on the stack
                virtCall, // Coppied virtual call to CardModel.Keyword.Contains
                new CodeInstruction(OpCodes.Brtrue_S, brTo), // Add a branch if true to skip the code that causes exhaust
            ]);
        
    }
}

// Here is the IL code for What I am patching
// IL_00a8: ldloc.1      // card
// IL_00a9: call         instance bool MegaCrit.Sts2.Core.Models.CardModel::get_ExhaustOnNextPlay()
// IL_00ae: brtrue.s     IL_00be
// 
// IL_00b0: ldloc.1      // card
// IL_00b1: call         instance class [System.Runtime]System.Collections.Generic.IReadOnlySet`1<valuetype MegaCrit.Sts2.Core.Entities.Cards.CardKeyword> MegaCrit.Sts2.Core.Models.CardModel::get_Keywords()
// IL_00b6: ldc.i4.1
// IL_00b7: callvirt     instance bool class [System.Runtime]System.Collections.Generic.IReadOnlySet`1<valuetype MegaCrit.Sts2.Core.Entities.Cards.CardKeyword>::Contains(!0/*valuetype MegaCrit.Sts2.Core.Entities.Cards.CardKeyword*/)
// IL_00bc: brfalse.s    IL_0123
// 
// Sample IL code for my patch
// IL_ff00: ldloc.1      // card
// IL_ff01: ldsfld       valuetype [sts2]MegaCrit.Sts2.Core.Entities.Cards.CardKeyword DeltaWarriors.DeltaWarriorsCode.Keywords.DeltaKeywords::Manual
// IL_ff02: callvirt     instance bool class [System.Runtime]System.Collections.Generic.IReadOnlySet`1<valuetype [sts2]MegaCrit.Sts2.Core.Entities.Cards.CardKeyword>::Contains(!0/*valuetype [sts2]MegaCrit.Sts2.Core.Entities.Cards.CardKeyword*/)
// IL_ff03: brfalse.t    IL_0123
