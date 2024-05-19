using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayToNight : MonoBehaviour
{
    [SerializeField] TMP_Text dayCountText;
    public void Start(){
        dayCountText.text = CostManager.dayCount.ToString() + " 일차";
    }
}