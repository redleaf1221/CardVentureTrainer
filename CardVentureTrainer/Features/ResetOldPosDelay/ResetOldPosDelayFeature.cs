using System.Reflection;
using BepInEx.Configuration;
using HarmonyLib;

namespace CardVentureTrainer.Features.ResetOldPosDelay;

public static class ResetOldPosDelayFeature {

    private static ConfigEntry<float> _configDelay;
    public static float Delay => _configDelay.Value;
    public static void Init() {
        _configDelay = Plugin.Config.Bind("Trainer", "ResetOldPosDelay",
            0.1f, "Adjust delay of resetting oldPos to make parrying easier or harder.");
        if (Delay < 0) _configDelay.Value = 0.1f;

        Plugin.HarmonyInstance.PatchAll(typeof(ResetOldPosDelayPatch));
        _configDelay.SettingChanged += (sender, args) => {
            Plugin.Logger.LogInfo($"ResetOldPosDelay changed to {Delay}.");
            Plugin.HarmonyInstance.Unpatch(AccessTools.EnumeratorMoveNext(typeof(UnitObjectPlayer).GetMethod(nameof(UnitObjectPlayer.ResetDodgeAfterDelay),
                    BindingFlags.NonPublic | BindingFlags.Instance)),
                typeof(ResetOldPosDelayPatch).GetMethod(nameof(ResetOldPosDelayPatch.Transpiler),
                    BindingFlags.Public | BindingFlags.Static));
            Plugin.HarmonyInstance.PatchAll(typeof(ResetOldPosDelayPatch));
        };
        Plugin.Logger.LogInfo("ResetOldPosDelayFeature loaded.");
    }
    public static bool TrySetDelay(float delay) {
        if (delay < 0) return false;
        _configDelay.Value = delay;
        return true;
    }
}
