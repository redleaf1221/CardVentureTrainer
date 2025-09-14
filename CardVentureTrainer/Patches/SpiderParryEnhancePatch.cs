using HarmonyLib;

namespace CardVentureTrainer.Patches;

//TODO Make attack desynchronized from animation.
// Because changing the animation itself just looks strange.
[HarmonyPatch]
public static class SpiderParryEnhancePatch {
    [HarmonyPostfix]
    [HarmonyPatch(typeof(UnitAtkStateJump), MethodType.Constructor,
        typeof(UnitObjectAbility), typeof(StateMachine), typeof(string), typeof(bool))]
    // ReSharper disable once InconsistentNaming
    private static void Constructor(ref UnitAtkStateJump __instance) {
        __instance.speed *= 2;
    }
}
