using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(UnitObjectOther), nameof(UnitObjectOther.WeaponDead))]
public static class DiamondShieldWeaponDeadPatch {
    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        List<CodeInstruction> codes = new(instructions);
        bool found = false;

        for (int i = 0; i < codes.Count - 3; i++) {
            if (codes[i].opcode == OpCodes.Ldarg_0 &&
                codes[i + 1].opcode == OpCodes.Ldfld &&
                codes[i + 1].operand?.ToString()?.Contains("weaponID") == true &&
                codes[i + 2].opcode == OpCodes.Ldc_I4 &&
                (int)codes[i + 2].operand == 1306 &&
                codes[i + 3].opcode == OpCodes.Bne_Un) {
                
                codes[i].opcode = OpCodes.Nop;
                codes[i].operand = null;
                codes[i + 1].opcode = OpCodes.Nop;
                codes[i + 1].operand = null;
                codes[i + 2].opcode = OpCodes.Nop;
                codes[i + 2].operand = null;
                codes[i + 3].opcode = OpCodes.Br;
                
                found = true;
                break;
            }
        }

        if (!found) {
            Plugin.Logger.LogError("Failed to patch UnitObjectOther.WeaponDead!!");
        }
        
        return codes.AsEnumerable();
    }
}