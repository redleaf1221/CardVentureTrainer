using CardVentureTrainer.Utils;
using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch]
public static class HighlightPatch {
    [HarmonyPostfix]
    [HarmonyPatch(typeof(LatticeNode), nameof(LatticeNode.Start))]
    // ReSharper disable once InconsistentNaming
    private static void Postfix(LatticeNode __instance) {
        __instance.gameObject.AddComponent<LatticeNodeHighlighter>();
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(LatticeObject), nameof(LatticeObject.ChapterBoardInit))]
    // ReSharper disable once InconsistentNaming
    private static bool Prefix(LatticeObject __instance) {
        LatticeNodeHighlighterCache.ClearCache();
        return true;
    }

    public static void InitPatch() {
        HarmonyInstance.PatchAll(typeof(HighlightPatch));
        Logger.LogInfo("HighlightPatch done.");
    }
}
