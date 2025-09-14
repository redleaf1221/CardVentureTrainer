using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(RoomAbility), nameof(RoomAbility.InLifeAbilityRoom))]
public static class EasterEggLifePatch {
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldc_R4, 0.05f)
            )
            .ThrowIfInvalid("Failed to patch RoomAbility.InLifeAbilityRoom!!")
            .SetOperandAndAdvance(0.4f)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldc_R4, 0.1f)
            )
            .ThrowIfInvalid("Failed to patch RoomAbility.InLifeAbilityRoom!!")
            .SetOperandAndAdvance(0.8f)
            .InstructionEnumeration();
    }
}
