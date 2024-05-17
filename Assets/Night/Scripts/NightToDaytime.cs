using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextDay : MonoBehaviour
{
    [SerializeField] TMP_Text dayCountText;
    [SerializeField] GameObject nextDayButton;
    public void Start(){
        Time.timeScale = 1;
        nextDayButton.SetActive(true);
        CostManager.dayCount++;
        dayCountText.text = CostManager.dayCount.ToString() + " 일차";
    }
}
