using CardVentureTrainer.Features.Highlight;
using HarmonyLib;
using UnityEngine;

namespace CardVentureTrainer.Features.ShowOldPos;

[HarmonyPatch]
public static class ShowOldPosPatch {
    private static Vector2Int _highlightPlayerOldPos;
    private static Vector2Int _highlightPlayerPos;
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(UnitObjectPlayer), nameof(UnitObjectPlayer.Update))]
    // ReSharper disable once InconsistentNaming
    public static void PlayerUpdatePrefix(UnitObjectPlayer __instance) {
        if (!ShowOldPosFeature.Enabled) {
            if (_highlightPlayerPos != Vector2Int.zero) {
                HighlightFeature.UnhighlightLattice(_highlightPlayerPos);
                _highlightPlayerPos = Vector2Int.zero;
            }
            if (_highlightPlayerOldPos != Vector2Int.zero) {
                HighlightFeature.UnhighlightLattice(_highlightPlayerOldPos);
                _highlightPlayerOldPos = Vector2Int.zero;
            }
            return;
        }
        if (_highlightPlayerPos != __instance.unitPos) {
            // This will cancel oldPos highlight so we put this front.
            HighlightFeature.UnhighlightLattice(_highlightPlayerPos);
            HighlightFeature.HighlightLattice(__instance.unitPos, new Color(1, 0, 0, 0.5f));
            _highlightPlayerPos = __instance.unitPos;
        }
        if (_highlightPlayerOldPos != __instance.oldPos) {
            HighlightFeature.UnhighlightLattice(_highlightPlayerOldPos);
            HighlightFeature.HighlightLattice(__instance.oldPos, new Color(0, 1, 0, 0.5f));
            _highlightPlayerOldPos = __instance.oldPos;
        }
    }
}
