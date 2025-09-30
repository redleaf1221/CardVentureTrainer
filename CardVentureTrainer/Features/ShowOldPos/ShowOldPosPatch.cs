using CardVentureTrainer.Features.Highlight;
using HarmonyLib;
using UnityEngine;

namespace CardVentureTrainer.Features.ShowOldPos;

[HarmonyPatch]
public static class ShowOldPosPatch {
    public static Vector2Int HighlightPlayerOldPos;
    public static Vector2Int HighlightPlayerPos;
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(UnitObjectPlayer), nameof(UnitObjectPlayer.Update))]
    // ReSharper disable once InconsistentNaming
    public static void PlayerUpdatePrefix(UnitObjectPlayer __instance) {
        if (!ShowOldPosFeature.Enabled) return;
        if (HighlightPlayerPos != __instance.unitPos) {
            // This will cancel oldPos highlight so we put this front.
            HighlightFeature.UnhighlightLattice(HighlightPlayerPos);
            HighlightFeature.HighlightLattice(__instance.unitPos, new Color(1, 0, 0, 0.5f));
            HighlightPlayerPos = __instance.unitPos;
        }
        if (HighlightPlayerOldPos != __instance.oldPos) {
            HighlightFeature.UnhighlightLattice(HighlightPlayerOldPos);
            HighlightFeature.HighlightLattice(__instance.oldPos, new Color(0, 1, 0, 0.5f));
            HighlightPlayerOldPos = __instance.oldPos;
        }
    }
}
