using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Night2Day : MonoBehaviour
{
    [SerializeField] GameObject BlackSquare;
    [SerializeField] GameObject MaskObject;
    GameObject NightButton;
    GameObject DrawButton;
    void Start()
    {
        NightButton = GameObject.Find("Night Button");
        DrawButton = GameObject.Find("Draw Button");
        MaskObject.SetActive(true);
        BlackSquare.SetActive(true);
        NightButton.GetComponent<BoxCollider2D>().enabled = false;
        DrawButton.GetComponent<BoxCollider2D>().enabled = false;
        MaskObject.transform.DOScale(new Vector3(0,0,0), 0.000001f);
        MaskObject.transform.DOScale(new Vector3(30,30,1), 1f).OnComplete(() =>
        {
            MaskObject.SetActive(false);
            BlackSquare.SetActive(false);
            NightButton.GetComponent<BoxCollider2D>().enabled = true;
            DrawButton.GetComponent<BoxCollider2D>().enabled = true;
        });
    }
}
