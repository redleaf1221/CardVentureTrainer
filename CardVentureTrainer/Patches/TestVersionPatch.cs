using BepInEx.Configuration;
using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(GameController), nameof(GameController.Awake))]
public static class TestVersionPatch {
    private static ConfigEntry<bool> _configEnabled;

    public static bool Enabled {
        get => _configEnabled.Value;
        set => _configEnabled.Value = value;
    }

    private static void Postfix() {
        GameSettings.testVersion = Enabled;
    }

    public static void InitPatch(Plugin plugin, Harmony harmony) {
        _configEnabled = plugin.Config.Bind("General", "EnableTestVersion",
            true, "Enable internal debug menu.");
        harmony.PatchAll(typeof(TestVersionPatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Logger.LogInfo($"EnableTestVersion changed to {Enabled}.");
            Logger.LogInfo("However this won't work until next restart.");
        };
        Logger.LogInfo("TestVersionPatch done.");
    }
}
