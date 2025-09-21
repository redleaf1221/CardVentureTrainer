using System.Collections;
using UnityEngine;

namespace CardVentureTrainer.Features.Highlight;

public class LatticeNodeHighlighter : MonoBehaviour {
    private SpriteRenderer _highlightRenderer;
    private Coroutine _highlightTimerCoroutine;
    private LatticeNode _latticeNode;

    private void Awake() {
        var highlightObj = new GameObject("LatticeHighlight");
        highlightObj.transform.SetParent(transform);
        highlightObj.transform.localPosition = Vector3.zero;
        highlightObj.transform.localScale = Vector3.one * 129f;

        _highlightRenderer = highlightObj.AddComponent<SpriteRenderer>();
        _highlightRenderer.sprite = CreateDefaultSprite();
        _highlightRenderer.color = Color.clear;
        _highlightRenderer.sortingOrder = 1000;
        _highlightRenderer.sortingLayerName = "card";
        _highlightRenderer.enabled = true;
        highlightObj.SetActive(true);

        _latticeNode = GetComponentInParent<LatticeNode>();
        LatticeNodeHighlighterCache.RegisterHighlighter(_latticeNode, this);
    }

    private void OnEnable() {
        HideHighlight();
    }

    private void OnDisable() {
        HideHighlight();
    }


    private void OnDestroy() {
        LatticeNodeHighlighterCache.UnregisterHighlighter(_latticeNode);
        if (_highlightRenderer && _highlightRenderer.gameObject) {
            Destroy(_highlightRenderer.gameObject);
        }
    }

    public void ShowHighlight(Color color, float duration = 0f) {
        if (_highlightTimerCoroutine != null) {
            StopCoroutine(_highlightTimerCoroutine);
            _highlightTimerCoroutine = null;
        }

        _highlightRenderer.color = color;

        if (duration > 0) {
            _highlightTimerCoroutine = StartCoroutine(HighlightTimer(duration));
        }
    }

    public void HideHighlight() {
        _highlightRenderer.color = Color.clear;
        if (_highlightTimerCoroutine == null) return;
        StopCoroutine(_highlightTimerCoroutine);
        _highlightTimerCoroutine = null;
    }

    private static Sprite CreateDefaultSprite() {
        var texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
    }

    private IEnumerator HighlightTimer(float duration) {
        yield return new WaitForSeconds(duration);
        if (_highlightTimerCoroutine == null) yield break;
        HideHighlight();
        _highlightTimerCoroutine = null;
    }
}
