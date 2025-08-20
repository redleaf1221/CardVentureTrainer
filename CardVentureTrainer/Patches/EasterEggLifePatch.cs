using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(RoomAbility), nameof(RoomAbility.InLifeAbilityRoom))]
public class EasterEggLifePatch {
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        List<CodeInstruction> codes = new(instructions);
        var found = 0;

        for (var i = 0; i < codes.Count; i++) {
            if (codes[i].opcode == OpCodes.Ldc_R4 &&
                Mathf.Approximately((float)codes[i].operand, 0.05f)) {
                codes[i].operand = 0.4f;
                found++;
            } else if (codes[i].opcode == OpCodes.Ldc_R4 &&
                       Mathf.Approximately((float)codes[i].operand, 0.1f)) {
                codes[i].operand = 0.8f;
                found++;
            }
        }

        if (found != 2) {
            Plugin.Logger.LogError("Failed to patch RoomAbility.InLifeAbilityRoom!!");
        }

        return codes.AsEnumerable();
    }
}
