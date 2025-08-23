using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(UnitObjectPlayer), nameof(UnitObjectPlayer.ResetDodgeAfterDelay), MethodType.Enumerator)]
public static class ResetOldPosDelayPatch {
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldc_R4, 0.1f)
            )
            .ThrowIfInvalid("Failed to patch UnitObjectPlayer.ResetDodgeAfterDelay!!")
            .SetOperandAndAdvance(Plugin.Conf.ResetOldPosDelay)
            .InstructionEnumeration();
    }
}
