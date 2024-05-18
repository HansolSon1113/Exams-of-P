using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayToNight : MonoBehaviour
{
    public void dayToNight()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Night");
    }
}
