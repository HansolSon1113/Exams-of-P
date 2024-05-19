using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Night2DayAnimation : MonoBehaviour
{
    [SerializeField] GameObject BlackSqaure;
    [SerializeField] GameObject MaskObject;

    void Start() {
        MaskObject.transform.DOScale(new Vector3(30,30,1), 3f).OnComplete(() =>
        {
            BlackSqaure.SetActive(false);
            MaskObject.SetActive(false);
        });
    }
}
