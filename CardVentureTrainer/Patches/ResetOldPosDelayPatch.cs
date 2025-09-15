using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(UnitObjectPlayer), nameof(UnitObjectPlayer.ResetDodgeAfterDelay), MethodType.Enumerator)]
public static class ResetOldPosDelayPatch {
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldc_R4, 0.1f)
            )
            .ThrowIfInvalid("Failed to patch UnitObjectPlayer.ResetDodgeAfterDelay!!")
            .SetOperandAndAdvance(Conf.ConfigResetOldPosDelay.Value)
            .InstructionEnumeration();
    }
    public static void RegisterThis(Harmony harmony) {
        harmony.PatchAll(typeof(ResetOldPosDelayPatch));
        Conf.ConfigResetOldPosDelay.SettingChanged += (sender, args) => {
            Logger.LogInfo($"ResetOldPosDelay changed to {Conf.ConfigResetOldPosDelay.Value}.");
            harmony.Unpatch(AccessTools.EnumeratorMoveNext(typeof(UnitObjectPlayer).GetMethod(nameof(UnitObjectPlayer.ResetDodgeAfterDelay))),
                typeof(ResetOldPosDelayPatch).GetMethod(nameof(Transpiler)));
            harmony.PatchAll(typeof(ResetOldPosDelayPatch));
        };
        Logger.LogInfo("ResetOldPosDelayPatch done.");
    }
}
