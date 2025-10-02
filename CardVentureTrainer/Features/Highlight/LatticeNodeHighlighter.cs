using CardVentureTrainer.Core;
using UnityEngine;

namespace CardVentureTrainer.Features.Highlight;

public class LatticeNodeHighlighter : MonoBehaviour {
    private SpriteRenderer _highlightRenderer;
    private LatticeNode _latticeNode;

    private void Awake() {
        var highlightObj = new GameObject("LatticeHighlight");
        highlightObj.transform.SetParent(transform);
        highlightObj.transform.localPosition = Vector3.zero;
        highlightObj.transform.localScale = Vector3.one;

        _highlightRenderer = highlightObj.AddComponent<SpriteRenderer>();
        _highlightRenderer.sprite = SpriteManager.GetSprite("default");
        _highlightRenderer.color = Color.clear;
        _highlightRenderer.sortingOrder = 1000;
        _highlightRenderer.sortingLayerName = "card";
        _highlightRenderer.enabled = true;
        highlightObj.SetActive(true);

        _latticeNode = GetComponentInParent<LatticeNode>();
        LatticeNodeHighlighterCache.RegisterHighlighter(_latticeNode, this);
    }

    private void OnEnable() {
        ResetHighlight();
    }

    private void OnDisable() {
        ResetHighlight();
    }


    private void OnDestroy() {
        LatticeNodeHighlighterCache.UnregisterHighlighter(_latticeNode);
        if (_highlightRenderer && _highlightRenderer.gameObject) {
            Destroy(_highlightRenderer.gameObject);
        }
    }

    public void SetColor(Color color) {
        _highlightRenderer.color = color;
    }

    public void SetSprite(Sprite sprite) {
        _highlightRenderer.sprite = sprite;
    }

    public void ResetHighlight() {
        SetColor(Color.clear);
        SetSprite(SpriteManager.GetSprite("default"));
    }
}
