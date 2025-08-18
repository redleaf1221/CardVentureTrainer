using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using System.Reflection.Emit;

namespace CardVentureTrainer;

[HarmonyPatch(typeof(UnLockUI), nameof(UnLockUI.SetPrice))]
public static class DiamondShieldSetPricePatch {
    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        List<CodeInstruction> codes = new(instructions);
        bool found = false;

        for (int i = 0; i < codes.Count - 2; i++) {
            if (codes[i].opcode == OpCodes.Ldarg_2 &&
                codes[i + 1].opcode == OpCodes.Ldc_I4 &&
                (int)codes[i + 1].operand == 1306 &&
                codes[i + 2].opcode == OpCodes.Bne_Un) {
                
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
            Plugin.Logger.LogError("Failed to patch UnLockUI.SetPrice!!");
            return codes.AsEnumerable();
        }
        
        Plugin.Logger.LogMessage("DiamondShieldSetPricePatch succeed.");
        
        return codes.AsEnumerable();
    }
}