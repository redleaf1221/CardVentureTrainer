using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(BattleObject), nameof(BattleObject.GetRandomRoom))]
public class DemoRoomGetRandomRoomPatch {
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once RedundantAssignment
    private static bool Prefix(ref RoomType __result, List<RoomType> weightedPool) {
        RoomType roomType = weightedPool[Random.Range(0, weightedPool.Count)];
        if (roomType != RoomType.Life) {
            __result = roomType;
            return false;
        }
        var randomValue = Random.value;
        if (randomValue >= 0.9f) {
            __result = RoomType.Life;
            return false;
        }
        var remappedValue = randomValue / 0.9f;
        if (remappedValue < 0.2f) {
            __result = RoomType.Coin;
        }
        else if (remappedValue < 0.4f) {
            __result = RoomType.Puzzle;
        }
        else if (remappedValue < 0.6f) {
            __result = RoomType.Cat;
        }
        else if (remappedValue < 0.8f) {
            __result = RoomType.Soul;
        }
        else {
            __result = RoomType.Apple;
        }
        return false;
    }
}