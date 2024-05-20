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

    public void Day2NightCircle() {
        BlackSqaure.SetActive(true);
        MaskObject.SetActive(true);
        NightButton.GetComponent<BoxCollider2D>().enabled = false;
        DrawButton.GetComponent<BoxCollider2D>().enabled = false;
        MaskObject.transform.DOScale(new Vector3(0,0,1), 3f).OnComplete(() =>
        {
            SceneManager.LoadScene("Night");
        });
    }

    private void OnMouseDown()
    {
        if (gameObject.name == "Night Button")
        {
            Audio.Inst.playSceneChange();
            Day2NightCircle();
        }
        else if (gameObject.name == "Draw Button")
        {
            Audio.Inst.playDraw();
            CardManager.Inst.draw();
        }
    }
}
