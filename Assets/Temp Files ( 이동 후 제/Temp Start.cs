using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempStart : MonoBehaviour
{
    public void Temp_start() {
        SceneManager.LoadScene("Day");
        CostManager.drawedCardCount = 0;
        CostManager.drawedMajor = 0;
        CostManager.drawedLib = 0;
        CostManager.drawedWork = 0;
        CostManager.drawedPlay = 0;
        CostManager.dayCount = 0;
        CostManager.startTime = 6;
        CostManager.MP = 100;
    }
}
