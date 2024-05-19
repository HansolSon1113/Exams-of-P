using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject timerObj;
    [SerializeField] GameObject nightEndPanel;

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
        NightCardManager.Inst.Night2Day_Circle();
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
