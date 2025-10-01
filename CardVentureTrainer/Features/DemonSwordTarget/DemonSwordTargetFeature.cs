using System.Collections.Generic;
using BepInEx.Configuration;
using CardVentureTrainer.Features.Highlight;
using UnityEngine;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Features.DemonSwordTarget;

public static class DemonSwordTargetFeature {

    private static ConfigEntry<bool> _configEnabled;

    public static bool Enabled {
        get => _configEnabled.Value;
        set => _configEnabled.Value = value;
    }

    public static void Init() {
        _configEnabled = Config.Bind("Trainer", "DemonSwordTarget",
            false, "Show targets of demon sword to avoid metaphysics.");

        HarmonyInstance.PatchAll(typeof(DemonSwordTargetPatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Plugin.Logger.LogInfo($"DemonSwordTarget changed to {Enabled}.");
            foreach (Vector2Int direction in DemonSwordTargetHelper.Directions) {
                HighlightFeature.Unhighlight(DemonSwordTargetPatch.HighlightTargets[direction]);
                DemonSwordTargetPatch.HighlightTargets[direction] = Vector2Int.zero;
            }
        };
        Plugin.Logger.LogInfo("DemonSwordTargetFeature loaded.");
    }

}
