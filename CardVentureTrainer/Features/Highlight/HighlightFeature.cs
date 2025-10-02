using CardVentureTrainer.Core;
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

    public static void Highlight(Vector2Int position, Color color, Sprite sprite) {
        LatticeNodeHighlighter highlighter = GetLatticeHighlighter(position);
        highlighter.SetColor(color);
        highlighter.SetSprite(sprite);
    }

    public static void Highlight(Vector2Int position, Color color) {
        LatticeNodeHighlighter highlighter = GetLatticeHighlighter(position);
        highlighter.SetColor(color);
        highlighter.SetSprite(SpriteManager.GetSprite("default"));
    }

    public static void Unhighlight(Vector2Int position) {
        LatticeNodeHighlighter highlighter = GetLatticeHighlighter(position);
        highlighter.ResetHighlight();
    }

    public static void Init() {
        HarmonyInstance.PatchAll(typeof(HighlightPatch));
        Plugin.Logger.LogInfo("HighlightFeature loaded.");
    }
}
