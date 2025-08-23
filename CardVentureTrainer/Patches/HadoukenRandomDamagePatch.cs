using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(UnitObjectPlayer), nameof(UnitObjectPlayer.PlayerInputCheck))]
public static class HadoukenRandomDamagePatch {
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(CodeInstruction.Call(typeof(Random), "get_value")),
                new CodeMatch(OpCodes.Ldc_R4, 0.2f),
                new CodeMatch(OpCodes.Bge_Un)
            )
            .ThrowIfInvalid("Failed to patch UnitObjectPlayer.PlayerInputCheck!!")
            .SetAndAdvance(OpCodes.Nop, null)
            .SetAndAdvance(OpCodes.Nop, null)
            .SetOpcodeAndAdvance(OpCodes.Br)
            .InstructionEnumeration();
    }
}
