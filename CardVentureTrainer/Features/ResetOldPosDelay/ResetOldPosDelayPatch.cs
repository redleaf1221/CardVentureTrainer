using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace CardVentureTrainer.Features.ResetOldPosDelay;

[HarmonyPatch(typeof(UnitObjectPlayer), nameof(UnitObjectPlayer.ResetDodgeAfterDelay), MethodType.Enumerator)]
public static class ResetOldPosDelayPatch {
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldc_R4, 0.1f)
            )
            .ThrowIfInvalid("Failed to patch UnitObjectPlayer.ResetDodgeAfterDelay!!")
            .SetOperandAndAdvance(ResetOldPosDelayFeature.Delay)
            .InstructionEnumeration();
    }
}
