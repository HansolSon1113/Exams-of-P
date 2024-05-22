using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

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
    void Start()
    {
        StartCoroutine(RunEndingSequence());
    }

    private IEnumerator RunEndingSequence()
    {
        SpriteRenderer cutScene;
        if (CostManager.MP <= 0)
        {
            cutScene = burnoutCutScene;
            normalCutScene.DOFade(0f, 0f);
        }
        else
        {
            cutScene = normalCutScene;
            burnoutCutScene.DOFade(0f, 0f);
        }
        Time.timeScale = 1f;
        overPanel.DOFade(0f, 1.5f);
        Audio.Inst.playNormalEndingCut();
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
        yield return StartCoroutine(Delay(1f));
        Audio.Inst.playEndingWrite();
        lib.SetActive(true);
        lib.GetComponent<TMP_Text>().text = CostManager.drawedLib.ToString();
        yield return StartCoroutine(Delay(1f));
        Audio.Inst.playEndingWrite();
        work.SetActive(true);
        work.GetComponent<TMP_Text>().text = CostManager.drawedWork.ToString();
        yield return StartCoroutine(Delay(1f));
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
        yield return StartCoroutine(Delay(2f));
        Audio.Inst.playEndingStamp();
        majorGrade.sprite = sprites[CostManager.drawedMajor];
        majorGrade.DOColor(new Color(1, 1, 1, 1), 0f);
        yield return StartCoroutine(Delay(2f));
        Audio.Inst.playEndingStamp();
        libGrade.sprite = sprites[CostManager.drawedLib];
        libGrade.DOColor(new Color(1, 1, 1, 1), 0f);
        yield break;
    }
}
