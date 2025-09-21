using System.Collections.Generic;
using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Features.SchoolDataOverride;

[HarmonyPatch(typeof(BattleObject), nameof(BattleObject.InitAbilityPool))]
public static class SchoolDataOverridePatch {
    // ReSharper disable once InconsistentNaming
    public static void Prefix(BattleObject __instance) {
        // to bypass index out of range without transpiler.
        // I'm lazy.
        if (SchoolDataOverrideFeature.SchoolData.Count == 0) return;
        __instance.schoolDataNow = new List<int>([1200, 1201, 1202, 1299]);
    }

    // ReSharper disable once InconsistentNaming
    public static void Postfix(BattleObject __instance) {
        if (SchoolDataOverrideFeature.SchoolData.Count <= 0) return;
        Logger.LogInfo("schoolDataNow patched!");
        __instance.schoolDataNow = new List<int>(SchoolDataOverrideFeature.SchoolData);
    }
}
