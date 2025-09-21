using BepInEx.Configuration;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Features.ParrySide;

public static class ParrySideFeature {
    private static ConfigEntry<bool> _configEnabled;

    public static bool Enabled {
        get => _configEnabled.Value;
        set => _configEnabled.Value = value;
    }

    public static void Init() {
        _configEnabled = Config.Bind("Trainer", "EnableParrySide",
            false, "Allow parrying from sides.");

        HarmonyInstance.PatchAll(typeof(ParrySidePatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Logger.LogInfo($"EnableParrySide changed to {Enabled}.");
            BattleObject.Instance.canParrySide = Enabled;
        };
        Logger.LogInfo("ParrySideFeature loaded.");
    }
}
