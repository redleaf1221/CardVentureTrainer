using System;
using System.Collections.Generic;

namespace CardVentureTrainer.Features.Highlight;

public static class LatticeNodeHighlighterCache {
    private static readonly Dictionary<LatticeNode, WeakReference<LatticeNodeHighlighter>> NodeHighlighterCache = new();

    public static void RegisterHighlighter(LatticeNode node, LatticeNodeHighlighter highlighter) {
        NodeHighlighterCache[node] = new WeakReference<LatticeNodeHighlighter>(highlighter);
    }

    public static void UnregisterHighlighter(LatticeNode node) {
        NodeHighlighterCache.Remove(node);
    }

    public static LatticeNodeHighlighter TryGetNodeHighlighter(LatticeNode node) {
        if (!NodeHighlighterCache.TryGetValue(node, out WeakReference<LatticeNodeHighlighter> reference)) return null;
        if (reference.TryGetTarget(out LatticeNodeHighlighter highlighter)) {
            return highlighter;
        }
        NodeHighlighterCache.Remove(node);
        return null;
    }

    public static void HideHighlightForAllNodes() {
        foreach (WeakReference<LatticeNodeHighlighter> reference in NodeHighlighterCache.Values) {
            if (!reference.TryGetTarget(out LatticeNodeHighlighter highlighter)) return;
            highlighter.HideHighlight();
        }
    }

    public static void ClearCache() {
        NodeHighlighterCache.Clear();
    }
}
