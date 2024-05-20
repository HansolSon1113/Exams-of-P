using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject BlackSqaure;
    [SerializeField] GameObject MaskObject;

    void Start() {

    }

    private void Day2NightCircle() {
        BlackSqaure.SetActive(true);
        MaskObject.SetActive(true);
        MaskObject.transform.DOScale(new Vector3(0,0,1), 3f).OnComplete(() =>
        {
            SceneManager.LoadScene("Night");
        });
    }

    private void OnMouseDown()
    {
        if(gameObject.name == "Night Button")
        {
            Time.timeScale = 1f;
            Audio.Inst.playSceneChange();
            CardManager.Inst.END();
            Day2NightCircle();
        }
        if(gameObject.name == "Draw Button")
        {
            Audio.Inst.playDraw();
            CardManager.Inst.draw();
        }
    }
}
