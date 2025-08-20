using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(BattleObject), nameof(BattleObject.GetRandomRoom))]
public static class DemoRoomGetRandomRoomPatch {
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once RedundantAssignment
    private static bool Prefix(ref RoomType __result, List<RoomType> weightedPool) {
        RoomType roomType = weightedPool[Random.Range(0, weightedPool.Count)];
        if (roomType != RoomType.Life) {
            __result = roomType;
            return false;
        }
        __result = Random.value switch {
            < 0.17f => RoomType.Life,
            < 0.33f => RoomType.Coin,
            < 0.5f => RoomType.Puzzle,
            < 0.66f => RoomType.Cat,
            < 0.83f => RoomType.Soul,
            _ => RoomType.Apple
        };
        return false;
    }
}
