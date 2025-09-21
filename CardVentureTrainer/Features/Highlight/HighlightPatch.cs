using HarmonyLib;

namespace CardVentureTrainer.Features.Highlight;

[HarmonyPatch]
public static class HighlightPatch {
    [HarmonyPostfix]
    [HarmonyPatch(typeof(LatticeNode), nameof(LatticeNode.Start))]
    // ReSharper disable once InconsistentNaming
    private static void StartPostfix(LatticeNode __instance) {
        __instance.gameObject.AddComponent<LatticeNodeHighlighter>();
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(LatticeObject), nameof(LatticeObject.ChapterBoardInit))]
    // ReSharper disable once InconsistentNaming
    private static bool ChapterBoardInitPrefix(LatticeObject __instance) {
        LatticeNodeHighlighterCache.HideHighlightForAllNodes();
        return true;
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(BattleObject), nameof(BattleObject.runStartLevel))]
    // ReSharper disable once InconsistentNaming
    private static bool Prefix(LatticeObject __instance) {
        LatticeNodeHighlighterCache.HideHighlightForAllNodes();
        return true;
    }
}
