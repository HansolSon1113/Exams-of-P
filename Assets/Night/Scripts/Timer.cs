using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject BlackSqaure;
    [SerializeField] GameObject MaskObject;
    private int time = 30;
    GameObject DayButton;

    private void Start()
    {
        InvokeRepeating("timer", 0, 1);
        DayButton = GameObject.Find("Day Button");
    }

    private void SceneChanger()
    {
        Audio.Inst.playSceneChange();
        NightCardManager.Inst.END();
        CostManager.dayCount++;
        BlackSqaure.SetActive(true);
        MaskObject.SetActive(true);
        MaskObject.transform.localScale = new Vector3(30, 30, 1);
        MaskObject.transform.position = new Vector3(7.4f, -3.1f, 0);
        DayButton.GetComponent<BoxCollider2D>().enabled = false;
        MaskObject.transform.DOScale(new Vector3(0, 0, 0), 1f).OnComplete(() =>
            {
                SceneManager.LoadScene("Day");
            });
    }

    private void OnMouseDown()
    {
        CancelInvoke("timer");
        SceneChanger();
    }

    private void timer()
    {
        time--;
        if (time<= 0)
        {
            CancelInvoke("timer");
            SceneChanger();
        }
    }
}
