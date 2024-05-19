using System.Collections;
using DG.Tweening;
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
        BlackSqaure.SetActive(false);
        MaskObject.SetActive(false);
    }

    public void Day2NightCircle() {
        BlackSqaure.SetActive(true);
        MaskObject.SetActive(true);
        MaskObject.transform.DOScale(new Vector3(0,0,1), 3f).OnComplete(() =>
        {
            SceneManager.LoadScene("Night");
        });
    }

    private void OnMouseDown()
    {
        if (gameObject.name == "Night Button Circle")
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
