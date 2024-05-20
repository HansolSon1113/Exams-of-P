using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Night2Day : MonoBehaviour
{
    [SerializeField] GameObject BlackSquare;
    [SerializeField] GameObject MaskObject;
    void Start()
    {
        MaskObject.SetActive(true);
        BlackSquare.SetActive(true);
        MaskObject.transform.DOScale(new Vector3(0,0,0), 0.000001f);
        MaskObject.transform.DOScale(new Vector3(30,30,1), 3f).OnComplete(() =>
        {
            MaskObject.SetActive(false);
            BlackSquare.SetActive(false);
        });
    }
}
