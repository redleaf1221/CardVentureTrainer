using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

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
                if (!Plugin.Conf.DisableParryOldPosCheck) flag2 = false;
            }
            if (SingletonData<BattleObject>.Instance.playerObject.unitPos == vector2Int3) {
                flag3 = true;
            }
        }
        List<string> cantParryReasons = [];
        List<string> cantDodgeReasons = [];
        if (!flag2) cantParryReasons.Add("oldPos in targetPos");
        if (!flag3) cantParryReasons.Add("unitPos not in targetPos");
        if (SingletonData<BattleObject>.Instance.playerObject.oldPos == Vector2Int.zero) cantParryReasons.Add("oldPos is zero");
        if (SingletonData<BattleObject>.Instance.playerObject.aimDir != -__instance.aimDir &&
            !SingletonData<BattleObject>.Instance.canParrySide) cantParryReasons.Add("aimDir wrong");
        if (__instance.unitType == 204 || __instance.unitType == 205 || __instance.unitType == 220 || __instance.unitType == 209) {
            cantParryReasons.Add($"unitType is {__instance.unitType}");
            cantDodgeReasons.Add($"unitType is {__instance.unitType}");
        }
        if (__instance.unitCamp == UnitCamp.player) {
            cantParryReasons.Add("unitCamp is player");
            cantDodgeReasons.Add("unitCamp is player");
        }
        cantDodgeReasons.Add("unitHit contains player");
        if (!flag) cantDodgeReasons.Add("oldPos not in targetPos");
        Plugin.Logger.LogMessage($"Can't parry because: {cantParryReasons.Join()}");
        Plugin.Logger.LogMessage($"Can't dodge because: {cantDodgeReasons.Join()}");
    }
}
