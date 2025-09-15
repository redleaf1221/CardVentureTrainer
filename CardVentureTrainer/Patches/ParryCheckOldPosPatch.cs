using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(UnitObjectAbility), nameof(UnitObjectAbility.AddDamageRange))]
public static class ParryCheckOldPosPatch {
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        if (!Conf.ConfigDisableParryOldPosCheck.Value) {
            return instructions;
        }
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldc_I4_1),
                new CodeMatch(OpCodes.Stloc_3),
                new CodeMatch(OpCodes.Ldc_I4_0),
                new CodeMatch(OpCodes.Stloc_S)
            )
            .ThrowIfInvalid("Failed to patch UnitObjectAbility.AddDamageRange!!")
            .Advance(2)
            .SetAndAdvance(OpCodes.Nop, null)
            .SetAndAdvance(OpCodes.Nop, null)
            .InstructionEnumeration();
    }
    public static void RegisterThis(Harmony harmony) {
        harmony.PatchAll(typeof(ParryCheckOldPosPatch));
        Conf.ConfigDisableParryOldPosCheck.SettingChanged += (sender, args) => {
            Logger.LogInfo($"DisableParryOldPosCheck changed to {Conf.ConfigDisableParryOldPosCheck.Value}.");
            harmony.Unpatch(typeof(UnitObjectPlayer).GetMethod(nameof(UnitObjectAbility.AddDamageRange)),
                typeof(ParryCheckOldPosPatch).GetMethod(nameof(Transpiler)));
            harmony.PatchAll(typeof(ParryCheckOldPosPatch));
        };
        Logger.LogInfo("ParryCheckOldPosPatch done.");
    }
}
