using System.Collections.Generic;
using UnityEngine;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Features.Highlight;

public static class HighlightFeature {

    private static LatticeNodeHighlighter GetLatticeHighlighter(Vector2Int position) {
        LatticeObject latticeObject = SingletonData<LatticeObject>.Instance;
        if (latticeObject?.latticeNodes == null || !latticeObject.CheckInMap(position)) return null;

        LatticeNode latticeNode = latticeObject.latticeNodes[position.x, position.y];
        return latticeNode ? LatticeNodeHighlighterCache.TryGetNodeHighlighter(latticeNode) : null;
    }

    public static bool Highlight(Vector2Int position, Color color, float duration = 0f) {
        LatticeNodeHighlighter highlighter = GetLatticeHighlighter(position);
        if (!highlighter) return false;
        highlighter.ShowHighlight(color, duration);
        return true;
    }

    public static bool Unhighlight(Vector2Int position) {
        LatticeNodeHighlighter highlighter = GetLatticeHighlighter(position);
        if (!highlighter) return false;
        highlighter.HideHighlight();
        return true;
    }

    public static void Init() {
        HarmonyInstance.PatchAll(typeof(HighlightPatch));
        Plugin.Logger.LogInfo("HighlightFeature loaded.");
    }
}
