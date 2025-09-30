using System;
using System.Collections.Generic;
using System.Linq;
using CardVentureTrainer.Features.Highlight;
using HarmonyLib;
using UnityEngine;
using static CardVentureTrainer.Features.DemonSwordTarget.DemonSwordTargetFeature;

namespace CardVentureTrainer.Features.DemonSwordTarget;

[HarmonyPatch]
public static class DemonSwordTargetPatch {
    public static readonly Dictionary<Vector2Int, Vector2Int> HighlightTargets =
        Directions.ToDictionary(dir => dir, dir => Vector2Int.zero);

    [HarmonyPrefix]
    [HarmonyPatch(typeof(BattleObject), nameof(BattleObject.Update))]
    public static void UpdatePrefix() {
        if (!Enabled) return;
        if (BattleObject.Instance.playerObject == null) return;
        if (BattleObject.Instance.currentWeapon != 1313) return;
        Dictionary<Vector2Int, Vector2Int> newTargets =
            Directions.ToDictionary(dir => dir,
                dir => DemonSwordTargeting(dir, BattleObject.Instance.playerObject));
        foreach (Vector2Int direction in Directions) {
            if (newTargets[direction] == HighlightTargets[direction]) continue;
            HighlightFeature.UnhighlightLattice(HighlightTargets[direction]);
            HighlightFeature.HighlightLattice(newTargets[direction], DirectionColors[direction]);
            HighlightTargets[direction] = newTargets[direction];
        }
    }
}
