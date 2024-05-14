using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject timerObj;
    [SerializeField] GameObject nightEndPanel;

    private void Start()
    {
        InvokeRepeating("timer", 0, 0.01f);
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
