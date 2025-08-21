using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(UnitObjectPlayer), nameof(UnitObjectPlayer.PlayerInputCheck))]
public class HadoukenRandomDamagePatch {
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        List<CodeInstruction> codes = new(instructions);
        var found = false;

        for (var i = 0; i < codes.Count - 2; i++) {
            if (codes[i].opcode == OpCodes.Call &&
                codes[i].operand?.ToString()?.Contains("get_value") == true &&
                codes[i + 1].opcode == OpCodes.Ldc_R4 &&
                Mathf.Approximately((float)codes[i + 1].operand, 0.2f) &&
                codes[i + 2].opcode == OpCodes.Bge_Un) {
                codes[i].opcode = OpCodes.Nop;
                codes[i].operand = null;
                codes[i + 1].opcode = OpCodes.Nop;
                codes[i + 1].operand = null;
                codes[i + 2].opcode = OpCodes.Br;

                found = true;
                break;
            }
        }

        if (!found) {
            Plugin.Logger.LogError("Failed to patch UnitObjectPlayer.PlayerInputCheck!!");
        }

        return codes.AsEnumerable();
    }
}
