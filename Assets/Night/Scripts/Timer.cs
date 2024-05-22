using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject timerObj;
    [SerializeField] GameObject BlackSqaure;
    [SerializeField] GameObject MaskObject;
    GameObject DayButton;

    private void Start()
    {
        InvokeRepeating("timer", 0, 0.0185f);
        DayButton = GameObject.Find("Day Button");
    }

    private void SceneChanger() {
        Audio.Inst.playSceneChange();
        NightCardManager.Inst.END();
        CostManager.dayCount++;
        BlackSqaure.SetActive(true);
        MaskObject.SetActive(true);
        MaskObject.transform.localScale = new Vector3(30,30,1);
        MaskObject.transform.position = new Vector3(7.4f, -3.1f, 0);
        DayButton.GetComponent<BoxCollider2D>().enabled = false;
        MaskObject.transform.DOScale(new Vector3(0,0,0), 1f).OnComplete(() =>
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
        timerObj.transform.localScale = timerObj.transform.localScale - new Vector3(0.01f, 0, 0);

        if (timerObj.transform.localScale.x <= 0)
        {
            CancelInvoke("timer");
            SceneChanger();
        }
    }
}
