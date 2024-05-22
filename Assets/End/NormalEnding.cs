using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class NormalEnding : MonoBehaviour
{
    public TMP_Text major;
    public TMP_Text lib;
    public TMP_Text work;
    public TMP_Text play;
    public Image majorGrade;
    public Image libGrade;
    public List<Sprite> sprites;
    [SerializeField] SpriteRenderer overPanel;
    [SerializeField] SpriteRenderer normalCutScene;
    [SerializeField] SpriteRenderer burnoutCutScene;
    void Start()
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
        StartCoroutine(delay());
        Audio.Inst.playEndingWrite();
        major.setActive(true);
        major.text = CostManager.drawedMajor.ToString();
        StartCoroutine(delay());
        Audio.Inst.playEndingWrite();
        lib.setActive(true);
        lib.text = CostManager.drawedLib.ToString();
        StartCoroutine(delay());
        Audio.Inst.playEndingWrite();
        work.setActive(true);
        work.text = CostManager.drawedWork.ToString();
        StartCoroutine(delay());
        Audio.Inst.playEndingWrite();
        play.setActive(true);
        play.text = CostManager.drawedPlay.ToString();
        setGrade();
    }

    private IEnumerator delay()
    {
        yield return new WaitForSeconds(1.5f);
    }

    private void setGrade()
    {
        delay();
        Audio.Inst.playEndingStamp();
        majorGrade.sprite = sprites[CostManager.drawedMajor];
        majorGrade.DOColor(new Color(1, 1, 1, 1), 0f);
        delay();
        Audio.Inst.playEndingStamp();
        libGrade.sprite = sprites[CostManager.drawedLib];
        libGrade.DOColor(new Color(1, 1, 1, 1), 0f);
    }
}
