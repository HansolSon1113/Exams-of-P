using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public GameObject HourHand;
    public GameObject MinuteHand;
    private bool isRotating = false;

    public void moveClock(int time)
    {
        if (!isRotating && time >= 0)
        {
            StartCoroutine(RotateClock(time));
        }
    }

    private IEnumerator RotateClock(float targetTime)
    {
        isRotating = true;
        float targetRot = HourHand.transform.eulerAngles.z - 15*targetTime;
        float startTime = Time.time;
        float endTime = startTime + targetTime;

        while (Time.time < endTime)
        {
            HourHand.transform.Rotate(0, 0, -15 * Time.deltaTime);
            MinuteHand.transform.Rotate(0, 0, -360 * Time.deltaTime);
            yield return null;
        }

        HourHand.transform.eulerAngles = new Vector3(0, 0, targetRot);
        MinuteHand.transform.eulerAngles = new Vector3(0, 0, 0);
        isRotating = false;
    }

}


