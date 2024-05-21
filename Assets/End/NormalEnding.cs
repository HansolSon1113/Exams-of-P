using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NormalEnding : MonoBehaviour
{
    public TMP_Text major;
    public TMP_Text lib;
    public TMP_Text work;
    public TMP_Text play;
    public TMP_Text majorGrade;
    public TMP_Text libGrade;
    [SerializeField] SpriteRenderer overPanel;
    [SerializeField] SpriteRenderer cutScene;
    void Start()
    {
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
        major.text = "전공: " + CostManager.drawedMajor;
        lib.text = "교양: " + CostManager.drawedLib;
        work.text = "알바: " + CostManager.drawedWork;
        play.text = "여가: " + CostManager.drawedPlay;
        setGrade();
    }

    private void setGrade()
    {
        switch (CostManager.drawedMajor)
        {
            case 8:
                majorGrade.text = "A+";
                break;
            case 7:
                majorGrade.text = "A";
                break;
            case 6:
                majorGrade.text = "B+";
                break;
            case 5:
                majorGrade.text = "B";
                break;
            case 4:
                majorGrade.text = "C+";
                break;
            case 3:
                majorGrade.text = "C";
                break;
            case 2:
                majorGrade.text = "D+";
                break;
            case 1:
                majorGrade.text = "D";
                break;
            case 0:
                majorGrade.text = "F";
                break;
        }
        switch (CostManager.drawedLib)
        {
            case 8:
                libGrade.text = "A+";
                break;
            case 7:
                libGrade.text = "A";
                break;
            case 6:
                libGrade.text = "B+";
                break;
            case 5:
                libGrade.text = "B";
                break;
            case 4:
                libGrade.text = "C+";
                break;
            case 3:
                libGrade.text = "C";
                break;
            case 2:
                libGrade.text = "D+";
                break;
            case 1:
                libGrade.text = "D";
                break;
            case 0:
                libGrade.text = "F";
                break;
        }
    }
}
