using BepInEx.Configuration;
using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(BattleObject), nameof(BattleObject.SkillRefresh))]
public static class ParrySidePatch {
    private static ConfigEntry<bool> _configEnabled;

    public static bool Enabled {
        get => _configEnabled.Value;
        set => _configEnabled.Value = value;
    }

    // ReSharper disable once InconsistentNaming
    private static void Postfix(BattleObject __instance) {
        if (Enabled) {
            __instance.canParrySide = true;
        }
    }

    public static void InitConfig(Plugin plugin) {
        _configEnabled = plugin.Config.Bind("Trainer", "EnableParrySide",
            false, "Allow parrying from sides.");
    }

    public static void RegisterThis(Harmony harmony) {
        harmony.PatchAll(typeof(ParrySidePatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Logger.LogInfo($"EnableParrySide changed to {Enabled}.");
        };
        Logger.LogInfo("ParrySidePatch done.");
    }
}
