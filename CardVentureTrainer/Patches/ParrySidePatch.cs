using HarmonyLib;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(BattleObject), nameof(BattleObject.SkillRefresh))]
public static class ParrySidePatch {
    // ReSharper disable once InconsistentNaming
    private static void Postfix(BattleObject __instance) {
        __instance.canParrySide = true;
    }
}
