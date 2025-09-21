using HarmonyLib;

namespace CardVentureTrainer.Features.ParrySide;

[HarmonyPatch(typeof(BattleObject), nameof(BattleObject.SkillRefresh))]
public static class ParrySidePatch {

    // ReSharper disable once InconsistentNaming
    public static void Postfix(BattleObject __instance) {
        if (ParrySideFeature.Enabled) {
            __instance.canParrySide = true;
        }
    }
}
