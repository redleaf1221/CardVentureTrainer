using System.Collections.Generic;
using BepInEx.Configuration;
using CardVentureTrainer.Features.Highlight;
using UnityEngine;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Features.DemonSwordTarget;

public static class DemonSwordTargetFeature {
    public static readonly Vector2Int[] Directions = [Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right];

    public static readonly Dictionary<Vector2Int, Color> DirectionColors = new() {
        { Vector2Int.up, new Color(1, 0, 0, 0.5f) },
        { Vector2Int.down, new Color(1, 1, 0, 0.5f) },
        { Vector2Int.left, new Color(0, 0, 1, 0.5f) },
        { Vector2Int.right, new Color(0, 1, 0, 0.5f) }
    };

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
            foreach (Vector2Int direction in Directions) {
                HighlightFeature.UnhighlightLattice(DemonSwordTargetPatch.HighlightTargets[direction]);
                DemonSwordTargetPatch.HighlightTargets[direction] = Vector2Int.zero;
            }
        };
        Plugin.Logger.LogInfo("DemonSwordTargetFeature loaded.");
    }

    public static Vector2Int DemonSwordTargeting(Vector2Int dir, UnitObjectPlayer player) {
        Vector2Int vector2Int = player.haveEnemyInRange(dir);
        if (SingletonData<BattleObject>.Instance.currentRoom == RoomType.EVE) vector2Int = default;
        Vector2Int vector2Int2 = vector2Int - dir;
        if (SingletonData<BattleObject>.Instance.demonSwordFlashBack) vector2Int2 = vector2Int + dir;
        if (vector2Int != default && (SingletonData<LatticeObject>.Instance.CheckPosCanMove(vector2Int2) ||
                                      SingletonData<BattleObject>.Instance.demonSwordMustFlash)) {
            return vector2Int;
        }
        return Vector2Int.zero;
    }
}
