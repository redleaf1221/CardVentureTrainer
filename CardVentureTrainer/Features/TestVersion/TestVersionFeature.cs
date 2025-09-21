using BepInEx.Configuration;

namespace CardVentureTrainer.Features.TestVersion;

public static class TestVersionFeature {
    private static ConfigEntry<bool> _configEnabled;

    public static bool Enabled {
        get => _configEnabled.Value;
        set => _configEnabled.Value = value;
    }

    public static void Init() {
        _configEnabled = Plugin.Config.Bind("General", "EnableTestVersion",
            true, "Enable internal debug menu.");
        Plugin.HarmonyInstance.PatchAll(typeof(TestVersionPatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Plugin.Logger.LogInfo($"EnableTestVersion changed to {Enabled}.");
            Plugin.Logger.LogInfo("However this won't work until next restart.");
        };
        Plugin.Logger.LogInfo("TestVersionFeature loaded.");
    }
}