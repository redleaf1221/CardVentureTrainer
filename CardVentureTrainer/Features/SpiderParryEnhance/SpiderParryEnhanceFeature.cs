using BepInEx.Configuration;

namespace CardVentureTrainer.Features.SpiderParryEnhance;

public static class SpiderParryEnhanceFeature {
    private static ConfigEntry<bool> _configEnabled;

    public static bool Enabled {
        get => _configEnabled.Value;
        set => _configEnabled.Value = value;
    }

    public static void Init() {
        _configEnabled = Plugin.Config.Bind("Trainer", "SpiderParryEnhance",
            false, "Reduce animation time to make parrying spiders easier.");
        Plugin.HarmonyInstance.PatchAll(typeof(SpiderParryEnhancePatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Plugin.Logger.LogInfo($"SpiderParryEnhance changed to {Enabled}.");
        };
        Plugin.Logger.LogInfo("SpiderParryEnhanceFeature loaded.");
    }
}