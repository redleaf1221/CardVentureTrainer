using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(UnitObjectAbility), nameof(UnitObjectAbility.AddDamageRange))]
public static class ParryDebugPatch {
    // ReSharper disable once InconsistentNaming
    private static void Postfix(ref UnitObjectAbility __instance, List<Vector2Int> targetPos, ref List<UnitObject> unithit, int damage) {
        if (!unithit.Contains(SingletonData<BattleObject>.Instance.playerObject)) {
            return;
        }
        Plugin.Logger.LogMessage("Oops! Got hit, here's the reason:");
        var flag = false;
        var flag2 = true;
        var flag3 = false;
        foreach (Vector2Int vector2Int3 in targetPos) {
            if (SingletonData<BattleObject>.Instance.playerObject.oldPos == vector2Int3) {
                flag = true;
                if (ParryCheckOldPosPatch.Enabled) flag2 = false;
            }
            if (SingletonData<BattleObject>.Instance.playerObject.unitPos == vector2Int3) {
                flag3 = true;
            }
        }
        List<string> cantParryReasons = [];
        List<string> cantDodgeReasons = [];
        if (!flag2) cantParryReasons.Add("player oldPos in targetPos");
        if (!flag3) cantParryReasons.Add("player unitPos not in targetPos");
        if (SingletonData<BattleObject>.Instance.playerObject.oldPos == Vector2Int.zero) cantParryReasons.Add("player oldPos is zero");
        if (SingletonData<BattleObject>.Instance.playerObject.aimDir != -__instance.aimDir &&
            !SingletonData<BattleObject>.Instance.canParrySide) cantParryReasons.Add("player aimDir wrong");
        if (__instance.aimDir == default) cantParryReasons.Add("unit aimDir is default");
        if (__instance.unitType is 204 or 205 or 220 or 209) {
            cantParryReasons.Add($"unitType is {__instance.unitType}");
            cantDodgeReasons.Add($"unitType is {__instance.unitType}");
        }
        if (__instance.unitCamp == UnitCamp.player) {
            cantParryReasons.Add("unitCamp is player");
            cantDodgeReasons.Add("unitCamp is player");
        }
        cantDodgeReasons.Add("unitHit contains player");
        if (!flag) cantDodgeReasons.Add("player oldPos not in targetPos");
        Plugin.Logger.LogMessage($"Can't parry because: {cantParryReasons.Join()}");
        Plugin.Logger.LogMessage($"Can't dodge because: {cantDodgeReasons.Join()}");
    }

    public static void InitPatch() {
        HarmonyInstance.PatchAll(typeof(ParryDebugPatch));
        Plugin.Logger.LogInfo("ParryDebugPatch done.");
    }
}
