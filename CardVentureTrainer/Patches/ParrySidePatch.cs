using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(BattleObject), nameof(BattleObject.SkillRefresh))]
public static class ParrySidePatch {
    // ReSharper disable once InconsistentNaming
    private static void Postfix(BattleObject __instance) {
        if (Conf.ConfigAlwaysParrySide.Value) {
            __instance.canParrySide = true;
        }
    }

    public static void RegisterThis(Harmony harmony) {
        harmony.PatchAll(typeof(ParrySidePatch));
        Conf.ConfigAlwaysParrySide.SettingChanged += (sender, args) => {
            Logger.LogInfo($"EnableParrySide changed to {Conf.ConfigAlwaysParrySide.Value}.");
        };
        Logger.LogInfo("ParrySidePatch done.");
    }
}
