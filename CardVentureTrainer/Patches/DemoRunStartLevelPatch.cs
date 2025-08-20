using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(BattleObject), nameof(BattleObject.runStartLevel))]
public static class DemoRunStartLevelPatch {
    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        List<CodeInstruction> codes = new(instructions);
        bool found = false;

        for (int i = 0; i < codes.Count - 4; i++) {
            if (codes[i].opcode == OpCodes.Ldarg_0 &&
                codes[i + 1].opcode == OpCodes.Ldfld &&
                codes[i + 1].operand?.ToString()?.Contains("currentChapter") == true &&
                codes[i + 2].opcode == OpCodes.Call &&
                codes[i + 2].operand?.ToString()?.Contains("op_Implicit") == true &&
                codes[i + 3].opcode == OpCodes.Ldc_I4_2 &&
                codes[i + 4].opcode == OpCodes.Ble) {
                codes[i + 3].opcode = OpCodes.Ldc_I4_3;

                found = true;
                break;
            }
        }

        if (!found) {
            Plugin.Logger.LogError("Failed to patch BattleObject.runStartLevel!!");
        }

        return codes.AsEnumerable();
    }
}
