using BepInEx.Configuration;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Features.ShowOldPos;

public static class ShowOldPosFeature {
    private static ConfigEntry<bool> _configEnabled;

    public static bool Enabled {
        get => _configEnabled.Value;
        set => _configEnabled.Value = value;
    }

    public static void Init() {
        _configEnabled = Config.Bind("Trainer", "ShowOldPos",
            false, "Show oldPos for parrying.");
        
        HarmonyInstance.PatchAll(typeof(ShowOldPosPatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Logger.LogInfo($"ShowOldPos changed to {Enabled}.");
        };
        Logger.LogInfo("ShowOldPosFeature loaded.");
    }
}
