using System.Collections.Generic;
using System.Linq;
using CardVentureTrainer.Features.Highlight;
using HarmonyLib;
using UnityEngine;

namespace CardVentureTrainer.Features.DemonSwordTarget;

[HarmonyPatch]
public static class DemonSwordTargetPatch {
    public static readonly Dictionary<Vector2Int, Vector2Int> HighlightTargets =
        DemonSwordTargetHelper.Directions.ToDictionary(dir => dir, dir => Vector2Int.zero);

    [HarmonyPrefix]
    [HarmonyPatch(typeof(BattleObject), nameof(BattleObject.Update))]
    public static void UpdatePrefix() {
        if (!DemonSwordTargetFeature.Enabled) return;
        if (BattleObject.Instance.playerObject == null) return;
        if (BattleObject.Instance.currentWeapon != 1313) return;
        Dictionary<Vector2Int, Vector2Int> newTargets =
            DemonSwordTargetHelper.Directions.ToDictionary(dir => dir,
                dir => DemonSwordTargetHelper.DemonSwordTargeting(dir, BattleObject.Instance.playerObject));
        foreach (Vector2Int direction in DemonSwordTargetHelper.Directions) {
            if (newTargets[direction] == HighlightTargets[direction]) continue;
            HighlightFeature.Unhighlight(HighlightTargets[direction]);
            HighlightFeature.Highlight(newTargets[direction], DemonSwordTargetHelper.DirectionColors[direction]);
            HighlightTargets[direction] = newTargets[direction];
        }
    }
}
