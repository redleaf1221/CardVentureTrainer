using BepInEx.Configuration;
using CardVentureTrainer.Features.Highlight;
using UnityEngine;
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
            Plugin.Logger.LogInfo($"ShowOldPos changed to {Enabled}.");
            HighlightFeature.UnhighlightLattice(ShowOldPosPatch.HighlightPlayerPos);
            ShowOldPosPatch.HighlightPlayerPos = Vector2Int.zero;
            HighlightFeature.UnhighlightLattice(ShowOldPosPatch.HighlightPlayerOldPos);
            ShowOldPosPatch.HighlightPlayerOldPos = Vector2Int.zero;
        };
        Plugin.Logger.LogInfo("ShowOldPosFeature loaded.");
    }
}
