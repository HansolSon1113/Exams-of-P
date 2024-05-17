using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonHighlight : MonoBehaviour
{
    private Vector3 originalScale;
    void OnMouseEnter()
    {
        originalScale = transform.localScale;
        transform.DOScale(originalScale + new Vector3(0.1f, 0.1f, 0), 0.05f);
    }

    void OnMouseExit()
    {
        transform.DOScale(originalScale, 0.05f);
    }
}
