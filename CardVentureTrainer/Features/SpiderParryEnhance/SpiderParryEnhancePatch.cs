using BepInEx.Configuration;
using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Features.SpiderParryEnhance;

[HarmonyPatch]
public static class SpiderParryEnhancePatch {

    [HarmonyPostfix]
    [HarmonyPatch(typeof(UnitAtkStateJump), MethodType.Constructor,
        typeof(UnitObjectAbility), typeof(StateMachine), typeof(string), typeof(bool))]
    // ReSharper disable once InconsistentNaming
    public static void Constructor(ref UnitAtkStateJump __instance) {
        if (SpiderParryEnhanceFeature.Enabled) {
            __instance.speed *= 2;
        }
    }

}
