using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireEnding : MonoBehaviour
{
    [SerializeField] SpriteRenderer overPanel;
    [SerializeField] GameObject TitleButton;
    [SerializeField] GameObject RestartButton;

    private IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    void Start()
    {
        TitleButton.SetActive(false);
        RestartButton.SetActive(false);
        Time.timeScale = 1f;
        Audio.Inst.playFireEnding();
        overPanel.DOFade(0f, 1.5f);
        // 나중에 나오는 순서, 속도, 위치 수정
        TitleButton.SetActive(true);
        RestartButton.SetActive(true);
    }
}
