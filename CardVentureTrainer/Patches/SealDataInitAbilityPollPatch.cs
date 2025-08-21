using System.Collections.Generic;
using HarmonyLib;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(BattleObject), nameof(BattleObject.InitAbilityPool))]
public static class SealDataInitAbilityPollPatch {
    // ReSharper disable once InconsistentNaming
    private static void Prefix(BattleObject __instance) {
        // to bypass index out of range without transpiler.
        // I'm lazy.
        __instance.sealDataNow = new List<int>([1200, 1201, 1202, 1299]);
    }

    // ReSharper disable once InconsistentNaming
    private static void Postfix(BattleObject __instance) {
        __instance.sealDataNow = Plugin.Conf.SealDataList;
    }
}
