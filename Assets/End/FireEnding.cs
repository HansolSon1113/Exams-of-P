using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireEnding : MonoBehaviour
{
    [SerializeField] SpriteRenderer overPanel;
    [SerializeField] GameObject TitleButton;
    [SerializeField] GameObject RestartButton;
    [SerializeField] GameObject doorClosed;
    [SerializeField] GameObject doorOpened;
    [SerializeField] GameObject character;
    [SerializeField] Transform charLocation;
    [SerializeField] GameObject chatSmall;
    [SerializeField] GameObject chatBig;

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
        overPanel.DOFade(0f, 1.5f).OnComplete(() =>
        {
            // 나중에 나오는 순서, 속도, 위치 수정
            StartCoroutine(Delay(2f));
            doorClosed.SetActive(false);
            doorOpened.SetActive(true);
            chatBig.SetActive(true);
            chatBig.transform.DORotate(new Vector3(0, 0, 20), 0.15f).OnComplete(() =>
            {
                chatBig.transform.DORotate(new Vector3(0, 0, -20), 0.15f).OnComplete(() =>
                {
                    chatBig.transform.DORotate(new Vector3(0, 0, 0), 0.3f);
                });
            });
            character.SetActive(true);
            character.transform.DOMove(charLocation.position, 1f);
            character.transform.DOScale(charLocation.localScale, 1f).OnComplete(() =>
            {
                chatSmall.SetActive(true);
                StartCoroutine(Delay(1f));
                doorOpened.SetActive(false);
                doorClosed.SetActive(true);
                TitleButton.SetActive(true);
                RestartButton.SetActive(true);
            });
        });
    }
}
