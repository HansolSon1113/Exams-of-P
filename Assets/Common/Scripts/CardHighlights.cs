using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CardHighlights : MonoBehaviour
{
    private int currentOrder;
    private SpriteRenderer cardRenderer;
    private Vector3 originalScale;
    private bool firstActive = false;

    void Start() {
        cardRenderer = this.GetComponent<SpriteRenderer>();
        currentOrder = cardRenderer.sortingOrder;
    }

    void OnMouseOver() {
        if (firstActive == false) {
            originalScale = this.transform.localScale;
            firstActive = true;
        }
        if (SceneManager.GetActiveScene().name == "Day") {
            cardRenderer.sortingLayerName = "Cards";
            transform.DOScale(new Vector3(1.3f, 1.3f, 0), 0.05f);
            cardRenderer.sortingOrder = 100;
        }
    }

    void OnMouseExit() {
        if (SceneManager.GetActiveScene().name == "Day") {
            cardRenderer.sortingLayerName = "Cards";
            transform.DOScale(originalScale, 0.05f);
            cardRenderer.sortingOrder = currentOrder;
        }
    }
}
