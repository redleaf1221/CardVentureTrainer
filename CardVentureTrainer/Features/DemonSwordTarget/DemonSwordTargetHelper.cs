using System.Collections.Generic;
using UnityEngine;

namespace CardVentureTrainer.Features.DemonSwordTarget;

public static class DemonSwordTargetHelper {

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
    public static readonly Vector2Int[] Directions = [Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right];

    public static readonly Dictionary<Vector2Int, Color> DirectionColors = new() {
        { Vector2Int.up, new Color(1, 0, 0, 0.5f) },
        { Vector2Int.down, new Color(1, 1, 0, 0.5f) },
        { Vector2Int.left, new Color(0, 0, 1, 0.5f) },
        { Vector2Int.right, new Color(0, 1, 0, 0.5f) }
    };
}
