using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NextDay : MonoBehaviour
{
    [SerializeField] TMP_Text dayCountText;
    public void Start(){
        CostManager.dayCount++;
        dayCountText.text = CostManager.dayCount.ToString();
    }
    public void nextDay(){
        Time.timeScale = 1;
        NightCardManager.Inst.nightEndPanel.SetActive(true);
        SceneManager.LoadScene("TestScene");
    }
}
