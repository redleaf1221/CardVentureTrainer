using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(BattleObject), nameof(BattleObject.GetRandomRoom))]
public class DemoRoomGetRandomRoomPatch {
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once RedundantAssignment
    static bool Prefix(ref RoomType __result, List<RoomType> weightedPool) {
        RoomType roomType = weightedPool[Random.Range(0, weightedPool.Count)];
        if (roomType != RoomType.Life) {
            __result = roomType;
            return false;
        }
        float r = Random.value;
        if (r >= 0.9f) {
            __result = RoomType.Life;
        }
        float remappedValue = r / 0.9f;
        if (remappedValue < 0.2f) {
            __result = RoomType.Coin;
        }
        if (remappedValue < 0.4f) {
            __result = RoomType.Puzzle;
        }
        if (remappedValue < 0.6f) {
            __result = RoomType.Cat;
        }
        if (remappedValue < 0.8f) {
            __result = RoomType.Soul;
        }
        __result = RoomType.Apple;
        return false;
    }
}