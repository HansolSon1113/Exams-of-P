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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            timerObj.SetActive(false);
            NightCardManager.Inst.END();
            NightCardManager.Inst.nightEndPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void timer()
    {
        timerObj.transform.localScale = timerObj.transform.localScale - new Vector3(0.01f, 0, 0);

        if (timerObj.transform.localScale.x <= 0)
        {
            CancelInvoke("timer");
            nightEndPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
