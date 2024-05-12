using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public GameObject HourHand;
    public GameObject MinuteHand;
    public void moveClock(int time)
    {
        float currentRotation = HourHand.transform.eulerAngles.z;
        currentRotation = Mathf.Repeat(currentRotation, 360);
        float targetRotation = currentRotation - 15*time;
        if (targetRotation < 0) targetRotation += 360;
        StartCoroutine(RotateClock(targetRotation));
    }


    private IEnumerator RotateClock(float targetRotation)
    {
        while (Mathf.Abs(Mathf.DeltaAngle(HourHand.transform.eulerAngles.z, targetRotation)) > 0.01f)
        {
            HourHand.transform.Rotate(0, 0, -10 * Time.deltaTime);
            MinuteHand.transform.Rotate(0, 0, -240 * Time.deltaTime);
            yield return null;
        }
        MinuteHand.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}


