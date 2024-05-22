using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize : MonoBehaviour
{
    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject BlackSqaure;
    GameObject StartButton;

    void Start(){
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        DG.Tweening.DOTween.Init();
        Audio.Inst.playTitleBackground();
        BlackSqaure.SetActive(true);
        BlackSqaure.GetComponent<SpriteRenderer>().DOFade(0f,1f).OnComplete(() =>
        {
            BlackSqaure.SetActive(false);
            BlackSqaure.GetComponent<SpriteRenderer>().DOFade(1f, 0.00001f);
        });
        StartButton = GameObject.Find("Start Button");
    }

    void OnMouseDown()
    {
        Audio.Inst.playSettings();
        StartButton.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(FadeOutAndStartGame());
    }

    IEnumerator FadeOutAndStartGame(){
        yield return StartCoroutine(FadeOut());
        gameStart();
    }

    IEnumerator FadeOut(){
        BlackSqaure.SetActive(true);
        Color color = BlackSqaure.GetComponent<SpriteRenderer>().color;
        color.a = 0;
        BlackSqaure.GetComponent<SpriteRenderer>().color = color;
        float duration = 1f; // Fade out duration
        float elapsedTime = 0f;

        while(elapsedTime < duration){
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / duration);
            BlackSqaure.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }

        color.a = 1;
        BlackSqaure.GetComponent<SpriteRenderer>().color = color;
    }

    public void gameStart(){
        for(int i = 0; i < itemSO.items.Length; i++){
            itemSO.items[i].used = false;
            itemSO.items[i].pass = false;
        }
        CostManager.passedCards = new List<Item>();
        CostManager.MP = 100;
        CostManager.drawedCardCount = 0;
        CostManager.drawedMajor = 0;
        CostManager.drawedLib = 0;
        CostManager.drawedWork = 0;
        CostManager.drawedPlay = 0;
        CostManager.dayCount = 0;
        CostManager.startTime = 6f;
        SceneManager.LoadScene("Night");
    }
}
