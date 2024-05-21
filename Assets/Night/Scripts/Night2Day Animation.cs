using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Night2DayAnimation : MonoBehaviour
{
    [SerializeField] GameObject BlackSqaure;
    [SerializeField] GameObject MaskObject;
    GameObject DayButton;

    void Start() {
        DayButton = GameObject.Find("Day Button");
        DayButton.GetComponent<BoxCollider2D>().enabled = false;
        MaskObject.transform.DOScale(new Vector3(35,35,1), 1.5f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            BlackSqaure.SetActive(false);
            MaskObject.SetActive(false);
            DayButton.GetComponent<BoxCollider2D>().enabled = true;
        });
    }
}
