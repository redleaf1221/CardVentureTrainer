using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(UnitObjectAbility), nameof(UnitObjectAbility.AddDamageRange))]
public static class ParryCheckOldPosPatch {
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        List<CodeInstruction> codes = new(instructions);
        var found = false;

        for (var i = 0; i < codes.Count - 3; i++) {
            if (codes[i].opcode == OpCodes.Ldc_I4_1 &&
                codes[i + 1].opcode == OpCodes.Stloc_3 &&
                codes[i + 2].opcode == OpCodes.Ldc_I4_0 &&
                codes[i + 3].opcode == OpCodes.Stloc_S) {
                codes[i + 2].opcode = OpCodes.Nop;
                codes[i + 2].operand = null;
                codes[i + 3].opcode = OpCodes.Nop;
                codes[i + 3].operand = null;

                found = true;
                break;
            }
        }

        if (!found) {
            Plugin.Logger.LogError("Failed to patch UnitObjectAbility.AddDamageRange!!");
        }

        return codes.AsEnumerable();
    }
}
