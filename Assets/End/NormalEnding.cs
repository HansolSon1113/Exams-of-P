using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;

public class NormalEnding : MonoBehaviour
{
    public GameObject major;
    public GameObject lib;
    public GameObject work;
    public GameObject play;
    public Image majorGrade;
    public Image libGrade;
    public List<Sprite> sprites;
    [SerializeField] SpriteRenderer overPanel;
    [SerializeField] SpriteRenderer normalCutScene;
    [SerializeField] SpriteRenderer burnoutCutScene;
    [SerializeField] GameObject TitleButton;
    [SerializeField] GameObject RestartButton;
    [SerializeField] float speed = 1f;
    void Start()
    {
        TitleButton.SetActive(false);
        RestartButton.SetActive(false);
        StartCoroutine(RunEndingSequence());
    }

    private IEnumerator RunEndingSequence()
    {
        int audioType = 0;
        SpriteRenderer cutScene;
        if (CostManager.MP <= 0)
        {
            cutScene = burnoutCutScene;
            normalCutScene.DOFade(0f, 0f);
            audioType = 1;
        }
        else
        {
            cutScene = normalCutScene;
            burnoutCutScene.DOFade(0f, 0f);
        }
        overPanel.DOFade(0f, 1.5f);
        if(audioType == 1)
        {
            Audio.Inst.playBurnOutCut();
        }
        else
        {
            Audio.Inst.playNormalEndingCut();
        }
        Time.timeScale = 1;
        cutScene.DOFade(1f, 1.5f).OnComplete(() =>
        {
            cutScene.DOFade(1, 2f).OnComplete(() =>
            {
                cutScene.DOFade(0, 1.5f);
                Audio.Inst.playNormalEnding();
            });
        });
        yield return StartCoroutine(Delay(5f));
        Audio.Inst.playEndingWrite();
        major.SetActive(true);
        major.GetComponent<TMP_Text>().text = CostManager.drawedMajor.ToString();
        yield return StartCoroutine(Delay(0.6f * speed));
        Audio.Inst.playEndingWrite();
        lib.SetActive(true);
        lib.GetComponent<TMP_Text>().text = CostManager.drawedLib.ToString();
        yield return StartCoroutine(Delay(0.6f * speed));
        Audio.Inst.playEndingWrite();
        work.SetActive(true);
        work.GetComponent<TMP_Text>().text = CostManager.drawedWork.ToString();
        yield return StartCoroutine(Delay(0.6f * speed));
        Audio.Inst.playEndingWrite();
        play.SetActive(true);
        play.GetComponent<TMP_Text>().text = CostManager.drawedPlay.ToString();
        StartCoroutine(setGrade());
    }

    private IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    private IEnumerator setGrade()
    {
        yield return StartCoroutine(Delay(1.4f * speed));
        Audio.Inst.playEndingStamp();
        majorGrade.sprite = sprites[CostManager.drawedMajor];
        majorGrade.DOColor(new Color(1, 1, 1, 1), 0f);
        yield return StartCoroutine(Delay(1.4f * speed));
        Audio.Inst.playEndingStamp();
        libGrade.sprite = sprites[CostManager.drawedLib];
        libGrade.DOColor(new Color(1, 1, 1, 1), 0f);
        yield return StartCoroutine(Delay(0.5f));
        TitleButton.SetActive(true);
        yield return StartCoroutine(Delay(0.5f));
        RestartButton.SetActive(true);
        yield break;
    }
}
