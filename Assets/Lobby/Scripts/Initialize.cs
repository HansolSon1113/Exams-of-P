using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize : MonoBehaviour
{
    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject BlackSqaure;
    [SerializeField] GameObject LobbyImages;
    [SerializeField] GameObject Titles;
    [SerializeField] GameObject Clock;
    [SerializeField] GameObject character;
    [SerializeField] GameObject Book;
    [SerializeField] GameObject Science;

    GameObject StartButton;
    GameObject EndButton;
    Vector3 Locate = new Vector3(0, -0.33f, 0);
    bool AnimationComplete = false;

    void RotateAnimation(float a, float t) {
        Clock.transform.DORotate(new Vector3(0, 0, a), 0);
        character.transform.DORotate(new Vector3(0, 0, a), 0);
        Book.transform.DORotate(new Vector3(0, 0, a), 0);
        Science.transform.DORotate(new Vector3(0, 0, a), 0);
        Science.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), t).OnComplete(() =>
        {
            Clock.transform.DORotate(new Vector3(0, 0, -a), 0);
            character.transform.DORotate(new Vector3(0, 0, -a), 0);
            Book.transform.DORotate(new Vector3(0, 0, -a), 0);
            Science.transform.DORotate(new Vector3(0, 0, -a), 0);
            Science.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), t).OnComplete(() =>
            {
                AnimationComplete = true;
            });
        });
    }

    void ImagesStartAnimation() {
        LobbyImages.transform.position = new Vector3(0, 10, 0);
        LobbyImages.transform.DOMove(new Vector3(0, -1, 0), 0.5f).OnComplete(() => 
        {
            LobbyImages.transform.DOMove(new Vector3(0, -0.1f, 0), 0.2f).OnComplete(() =>
            {
                LobbyImages.transform.DOMove(Locate, 0.1f);
            });
        });
    }
    void TitlesStartAnimation() {
        Titles.transform.position = new Vector3(0, 10, 0);
        Titles.transform.DOMove(new Vector3(0, -0.8f, 0), 0.5f).OnComplete(() => 
        {
            Titles.transform.DOMove(new Vector3(0, 0.2f, 0), 0.2f).OnComplete(() =>
            {
                Titles.transform.DOMove(new Vector3(0,0,0), 0.1f);
            });
        });
    }
    
    void Start(){
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        DG.Tweening.DOTween.Init();
        Audio.Inst.playTitleBackground();
        BlackSqaure.SetActive(true);
        StartButton = GameObject.Find("Start Button");
        EndButton = GameObject.Find("End Button");
        StartButton.GetComponent<BoxCollider2D>().enabled = false;
        EndButton.GetComponent<BoxCollider2D>().enabled = false;
        ImagesStartAnimation();
        TitlesStartAnimation();
        BlackSqaure.GetComponent<SpriteRenderer>().DOFade(0f,1f).OnComplete(() =>
        {
            BlackSqaure.SetActive(false);
            BlackSqaure.GetComponent<SpriteRenderer>().DOFade(1f, 0);
            StartButton.GetComponent<BoxCollider2D>().enabled = true;
            EndButton.GetComponent<BoxCollider2D>().enabled = true;
            AnimationComplete = true;
        });
    }

    void Update() {
        if (AnimationComplete == true){
            AnimationComplete = false;
            RotateAnimation(7, 0.8f);
        }
            
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
        for(int i = 0; i < 3; i++)
        {
            CostManager.passedCards.Add(null);
        }
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
