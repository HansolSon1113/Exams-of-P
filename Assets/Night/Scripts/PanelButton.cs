using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("TestScene");
    }
}
