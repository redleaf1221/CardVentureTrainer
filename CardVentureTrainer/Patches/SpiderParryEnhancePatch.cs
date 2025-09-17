using BepInEx.Configuration;
using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

//TODO Make attack desynchronized from animation.
// Because changing the animation itself just looks strange.
[HarmonyPatch]
public static class SpiderParryEnhancePatch {
    private static ConfigEntry<bool> _configEnabled;

    public static bool Enabled {
        get => _configEnabled.Value;
        set => _configEnabled.Value = value;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(UnitAtkStateJump), MethodType.Constructor,
        typeof(UnitObjectAbility), typeof(StateMachine), typeof(string), typeof(bool))]
    // ReSharper disable once InconsistentNaming
    private static void Constructor(ref UnitAtkStateJump __instance) {
        if (Enabled) {
            __instance.speed *= 2;
        }
    }

    public static void InitPatch(Plugin plugin, Harmony harmony) {
        _configEnabled = plugin.Config.Bind("Trainer", "SpiderParryEnhance",
            false, "Reduce animation time to make parrying spiders easier.");
        harmony.PatchAll(typeof(SpiderParryEnhancePatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Logger.LogInfo($"SpiderParryEnhance changed to {Enabled}.");
        };
        Logger.LogInfo("SpiderParryEnhancePatch done.");
    }
}
