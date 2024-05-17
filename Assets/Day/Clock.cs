using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public GameObject HourHand;
    public GameObject MinuteHand;
    private bool isRotating = false;
    private List<float> waitList = new List<float>();

    // 회전 속도 조절
    public float power = 4f;

    private void Start()
    {
        HourHand.transform.eulerAngles = new Vector3(0, 0, 360 - CostManager.startTime * 15);
    }

    private void Update()
    {
        if(waitList.Count > 0 && isRotating == false)
        {
            StartCoroutine(RotateClock(waitList[0]));
            waitList.RemoveAt(0);
        }
    }

    public void moveClock(int time)
    {
        if(!isRotating && time >= 0)
        {
            StartCoroutine(RotateClock(time));
        }
        else if(isRotating && time >= 0)
        {
            waitList.Add(time);
        }
    }

    private IEnumerator RotateClock(float targetTime)
    {
        isRotating = true;
        float targetRot = HourHand.transform.eulerAngles.z - 15*targetTime;
        float startTime = Time.time;
        float endTime = startTime + (targetTime / power);

        while (Time.time < endTime)
        {
            HourHand.transform.Rotate(0, 0, -15 * Time.deltaTime * power);
            MinuteHand.transform.Rotate(0, 0, -360 * Time.deltaTime * power);
            yield return null;
        }

        HourHand.transform.eulerAngles = new Vector3(0, 0, targetRot);
        MinuteHand.transform.eulerAngles = new Vector3(0, 0, 0);
        isRotating = false;
    }

}


