using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace CardVentureTrainer.Patches;

[HarmonyPatch]
public static class DiamondShieldPatch {
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(BattleObject), nameof(BattleObject.AddWeapon))]
    private static IEnumerable<CodeInstruction> AddWeaponPatch(IEnumerable<CodeInstruction> instructions) {
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldarg_1),
                new CodeMatch(OpCodes.Ldc_I4, 1306),
                new CodeMatch(OpCodes.Bne_Un)
            )
            .ThrowIfInvalid("Failed to patch BattleObject.AddWeapon!!")
            .SetAndAdvance(OpCodes.Nop, null)
            .SetAndAdvance(OpCodes.Nop, null)
            .SetOpcodeAndAdvance(OpCodes.Br)
            .InstructionEnumeration();
    }

    [HarmonyTranspiler]
    [HarmonyPatch(typeof(UnLockUI), nameof(UnLockUI.SetPrice))]
    private static IEnumerable<CodeInstruction> SetPricePatch(IEnumerable<CodeInstruction> instructions) {
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldarg_2),
                new CodeMatch(OpCodes.Ldc_I4, 1306),
                new CodeMatch(OpCodes.Bne_Un)
            )
            .ThrowIfInvalid("Failed to patch UnLockUI.SetPrice!!")
            .SetAndAdvance(OpCodes.Nop, null)
            .SetAndAdvance(OpCodes.Nop, null)
            .SetOpcodeAndAdvance(OpCodes.Br)
            .InstructionEnumeration();
    }

    [HarmonyTranspiler]
    [HarmonyPatch(typeof(UnitObjectOther), nameof(UnitObjectOther.WeaponDead))]
    private static IEnumerable<CodeInstruction> WeaponDeadPatch(IEnumerable<CodeInstruction> instructions) {
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(CodeInstruction.LoadField(typeof(UnitObjectOther), "weaponID")),
                new CodeMatch(OpCodes.Ldc_I4, 1306),
                new CodeMatch(OpCodes.Bne_Un)
            )
            .ThrowIfInvalid("Failed to patch UnitObjectOther.WeaponDead!!")
            .SetAndAdvance(OpCodes.Nop, null)
            .SetAndAdvance(OpCodes.Nop, null)
            .SetAndAdvance(OpCodes.Nop, null)
            .SetOpcodeAndAdvance(OpCodes.Br)
            .InstructionEnumeration();
    }
}
