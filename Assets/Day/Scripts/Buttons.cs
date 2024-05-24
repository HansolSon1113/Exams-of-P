using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject BlackSqaure;
    [SerializeField] GameObject MaskObject;
    GameObject NightButton;
    GameObject DrawButton;

    void Start() {
        NightButton = GameObject.Find("Night Button");
        DrawButton = GameObject.Find("Draw Button");
    }

    private void OnMouseDown()
    {
        if (gameObject.name == "Night Button")
        {
            CardManager.Inst.Day2NightCircle();
        }
        else if (gameObject.name == "Draw Button")
        {
            Audio.Inst.playDraw();
            CardManager.Inst.draw();
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        DrawButton.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        DrawButton.GetComponent<BoxCollider2D>().enabled = true;
    }
}
