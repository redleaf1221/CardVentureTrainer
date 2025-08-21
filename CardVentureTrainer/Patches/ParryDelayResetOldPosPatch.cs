using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(UnitObjectPlayer), nameof(UnitObjectPlayer.ResetDodgeAfterDelay), MethodType.Enumerator)]
public static class ParryDelayResetOldPosPatch {
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        List<CodeInstruction> codes = new(instructions);
        var found = false;

        for (var i = 0; i < codes.Count; i++) {
            if (codes[i].opcode == OpCodes.Ldc_R4 &&
                Mathf.Approximately((float)codes[i].operand, 0.1f)) {
                codes[i].operand = 0.5f;

                found = true;
                break;
            }
        }

        if (!found) {
            Plugin.Logger.LogError("Failed to patch UnitObjectPlayer.ResetDodgeAfterDelay!!");
        }

        return codes.AsEnumerable();
    }
}
