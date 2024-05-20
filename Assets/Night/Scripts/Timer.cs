using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject timerObj;
    [SerializeField] GameObject nightEndPanel;
    [SerializeField] TMP_Text dayCountText;
    [SerializeField] GameObject BlackSqaure;
    [SerializeField] GameObject MaskObject;

    private void Start()
    {
        InvokeRepeating("timer", 0, 0.0185f);
    }

    private void OnMouseDown()
    {
        CancelInvoke("timer");
        timerObj.SetActive(false);
        Audio.Inst.playSceneChange();
        NightCardManager.Inst.END();
        Audio.Inst.playSceneChange();
        CostManager.dayCount++;
        BlackSqaure.SetActive(true);
        MaskObject.SetActive(true);
        MaskObject.transform.localScale = new Vector3(30,30,1);
        MaskObject.transform.position = new Vector3(7.4f, -3.1f, 0);
        dayCountText.text = CostManager.dayCount.ToString() + " 일차";
        Audio.Inst.playSceneChange();
        MaskObject.transform.DOScale(new Vector3(0,0,0), 3f).OnComplete(() =>
            {
                SceneManager.LoadScene("Day");
            });
    }

    private void timer()
    {
        timerObj.transform.localScale = timerObj.transform.localScale - new Vector3(0.01f, 0, 0);

        if (timerObj.transform.localScale.x <= 0)
        {
            CancelInvoke("timer");
            NightCardManager.Inst.END();
            nightEndPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
