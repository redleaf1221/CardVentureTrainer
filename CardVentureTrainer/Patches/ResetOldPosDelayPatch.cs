using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using BepInEx.Configuration;
using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(UnitObjectPlayer), nameof(UnitObjectPlayer.ResetDodgeAfterDelay), MethodType.Enumerator)]
public static class ResetOldPosDelayPatch {
    private static ConfigEntry<float> _configDelay;

    // ReSharper disable once MemberCanBePrivate.Global
    public static float Delay => _configDelay.Value;

    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldc_R4, 0.1f)
            )
            .ThrowIfInvalid("Failed to patch UnitObjectPlayer.ResetDodgeAfterDelay!!")
            .SetOperandAndAdvance(Delay)
            .InstructionEnumeration();
    }

    public static void InitPatch() {
        _configDelay = Config.Bind("Trainer", "ResetOldPosDelay",
            0.1f, "Adjust delay of resetting oldPos to make parrying easier or harder.");
        if (Delay < 0) _configDelay.Value = 0.1f;

        HarmonyInstance.PatchAll(typeof(ResetOldPosDelayPatch));
        _configDelay.SettingChanged += (sender, args) => {
            Logger.LogInfo($"ResetOldPosDelay changed to {Delay}.");
            HarmonyInstance.Unpatch(AccessTools.EnumeratorMoveNext(typeof(UnitObjectPlayer).GetMethod(nameof(UnitObjectPlayer.ResetDodgeAfterDelay),
                    BindingFlags.NonPublic | BindingFlags.Instance)),
                typeof(ResetOldPosDelayPatch).GetMethod(nameof(Transpiler),
                    BindingFlags.NonPublic | BindingFlags.Static));
            HarmonyInstance.PatchAll(typeof(ResetOldPosDelayPatch));
        };
        Logger.LogInfo("ResetOldPosDelayPatch done.");
    }

    public static bool TrySetDelay(float delay) {
        if (delay < 0) return false;
        _configDelay.Value = delay;
        return true;
    }
}
