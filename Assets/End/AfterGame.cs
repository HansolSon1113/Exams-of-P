using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class AfterGame : MonoBehaviour
{
    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject BlackSquare;
    [SerializeField] GameObject MaskObject;
    GameObject TitleButton;
    GameObject RestartButton;

    private void Start() {
        BlackSquare.SetActive(false);
        MaskObject.SetActive(false);
        TitleButton = GameObject.Find("Title Button");
        RestartButton = GameObject.Find("Restart Button");
        MaskObject.transform.localScale = new Vector3(30,30,0);

    }

    private void OnMouseDown() {
        if (gameObject.name == "Title Button") {
            MaskObject.transform.position = TitleButton.transform.position;
            MaskObject.SetActive(true);
            BlackSquare.SetActive(true);
            Audio.Inst.playSettings();
            MaskObject.transform.DOScale(new Vector3(0,0,0), 1f).OnComplete(() =>
            {
                SceneManager.LoadScene("Lobby");
            });
        }
        if (gameObject.name == "Restart Button") {
            MaskObject.transform.position = RestartButton.transform.position;
            MaskObject.SetActive(true);
            BlackSquare.SetActive(true);
            Audio.Inst.playSettings();
            MaskObject.transform.DOScale(new Vector3(0,0,0), 1f).OnComplete(() =>
            {
                gameStart();
            });
        }
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
