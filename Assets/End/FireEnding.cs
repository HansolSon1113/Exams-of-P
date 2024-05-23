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
    [SerializeField] GameObject dust1;
    [SerializeField] GameObject dust2;

    private IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    private void Start()
    {
        StartCoroutine(anim());
    }

    private IEnumerator anim()
    {
        TitleButton.SetActive(false);
        RestartButton.SetActive(false);
        Time.timeScale = 1f;
        Audio.Inst.playFireEnding();
        overPanel.DOFade(0f, 1.5f).OnComplete(() =>
        {
            // 나중에 나오는 순서, 속도, 위치 수정
            doorClosed.transform.DOScale(new Vector3(0.7f, 0.7f, 1), 0.15f).OnComplete(() =>
            {
                doorClosed.SetActive(false);
                doorOpened.SetActive(true);
                doorOpened.transform.DOScale(new Vector3(1.3f, 1.3f, 1), 0.15f).OnComplete(() =>
                {
                    doorOpened.transform.DOScale(new Vector3(0.7f, 0.7f, 1f), 0.15f).OnComplete(() =>
                    {
                        doorOpened.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
                        doorClosed.transform.DOScale(new Vector3(1f, 1f, 1), 0);
                    });
                });
            });

            chatBig.SetActive(true);
            chatBig.transform.DORotate(new Vector3(0, 0, 10), 0.15f).OnComplete(() =>
            {
                chatBig.transform.DORotate(new Vector3(0, 0, -10), 0.15f).OnComplete(() =>
                {
                    chatBig.transform.DORotate(new Vector3(0, 0, 0), 0.3f);
                });
            });
            character.SetActive(true);
            character.transform.DOMove(charLocation.position, 1f);
            character.transform.DOScale(charLocation.localScale, 1f).OnComplete(() =>
            {
            });
        });
        yield return new WaitForSeconds(3f);
        chatSmall.SetActive(true);
        chatSmall.transform.DORotate(new Vector3(0, 0, 10), 0.15f).OnComplete(() =>
        {
            chatSmall.transform.DORotate(new Vector3(0, 0, -10), 0.15f).OnComplete(() =>
            {
                chatSmall.transform.DORotate(new Vector3(0, 0, 0), 0.3f);
            });
        });
        yield return new WaitForSeconds(0.9f);
        doorOpened.SetActive(false);
        doorClosed.SetActive(true);
        dust1.GetComponent<SpriteRenderer>().DOFade(0, 1f);
        dust1.transform.DOScale(new Vector3(0.3f, 0.3f, 0), 1f).SetEase(Ease.OutBounce);
        dust2.GetComponent<SpriteRenderer>().DOFade(0, 1f);
        dust2.transform.DOScale(new Vector3(0.3f, 0.3f, 0), 1f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1f);
        TitleButton.SetActive(true);
        TitleButton.transform.DOMoveX(8.2f, 0.5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.7f);
        RestartButton.SetActive(true);
        RestartButton.transform.DOMoveX(8.2f, 0.5f).SetEase(Ease.OutBack);
        yield break;
    }
}
