using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasterEgg : MonoBehaviour
{
    private int EasterEggCount;
    void Start()
    {
        EasterEggCount = 0;
    }

    void OnMouseDown() {
        if (EasterEggCount >= 5) {
            SceneManager.LoadScene("Credit");
        }
        EasterEggCount++;
    }
}
