using HarmonyLib;
using static CardVentureTrainer.Plugin;

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
        if (Conf.ConfigSpiderParryEnhance.Value) {
            __instance.speed *= 2;
        }
    }

    public static void RegisterThis(Harmony harmony) {
        harmony.PatchAll(typeof(SpiderParryEnhancePatch));
        Conf.ConfigSpiderParryEnhance.SettingChanged += (sender, args) => {
            Logger.LogInfo($"SpiderParryEnhance changed to {Conf.ConfigSpiderParryEnhance.Value}.");
        };
        Logger.LogInfo("SpiderParryEnhancePatch done.");
    }
}
