using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private void OnMouseDown()
    {
        if(gameObject.name == "Night Button")
        {
            CardManager.Inst.END();
            SceneManager.LoadScene("Night");
        }
        if(gameObject.name == "Draw Button")
        {
            CardManager.Inst.draw();
        }
    }
}
