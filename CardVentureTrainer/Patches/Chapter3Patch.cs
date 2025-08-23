using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace CardVentureTrainer.Patches;

[HarmonyPatch]
public static class Chapter3Patch {
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(BattleObject), nameof(BattleObject.runChapterComplete))]
    private static IEnumerable<CodeInstruction> RunChapterCompletePatch(IEnumerable<CodeInstruction> instructions) {
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(CodeInstruction.LoadField(typeof(BattleObject), "currentChapter")),
                new CodeMatch(CodeInstruction.Call(typeof(SafeInt), "op_Implicit", [typeof(SafeInt)])),
                new CodeMatch(OpCodes.Ldc_I4_2),
                new CodeMatch(OpCodes.Blt)
            )
            .ThrowIfInvalid("Failed to patch BattleObject.runChapterComplete!!")
            .Advance(3)
            .SetOpcodeAndAdvance(OpCodes.Ldc_I4_3)
            .InstructionEnumeration();
    }

    [HarmonyTranspiler]
    [HarmonyPatch(typeof(BattleObject), nameof(BattleObject.runStartLevel))]
    private static IEnumerable<CodeInstruction> RunStartLevelPatch(IEnumerable<CodeInstruction> instructions) {
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(CodeInstruction.LoadField(typeof(BattleObject), "currentChapter")),
                new CodeMatch(CodeInstruction.Call(typeof(SafeInt), "op_Implicit", [typeof(SafeInt)])),
                new CodeMatch(OpCodes.Ldc_I4_2),
                new CodeMatch(OpCodes.Ble)
            )
            .ThrowIfInvalid("Failed to patch BattleObject.runStartLevel!!")
            .Advance(3)
            .SetOpcodeAndAdvance(OpCodes.Ldc_I4_3)
            .InstructionEnumeration();
    }
}
