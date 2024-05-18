using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject MaskObject;
    public GameObject BlackSquare;
    public GameObject ChangeScene;

    private void Start()
    {
        ChangeScene.SetActive(false);
        MaskObject.transform.localScale = new Vector3(10, 10, 1);
    }

    void Day2Night_Circle()
    {
        ChangeScene.SetActive(true);
        MaskObject.transform.DOScale(new Vector3(0, 0, 1), 3f).OnComplete(() =>
        {
            SceneManager.LoadScene("Night");
        });
    }

    IEnumerator Fade()
    {
        for (float f = 0f; f <= 1f; f += 0.1f)
        {
            Color bright = BlackSquare.GetComponent<SpriteRenderer>().color;
            bright.a = f;
            BlackSquare.GetComponent<SpriteRenderer>().color = bright;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Day2Night_Bright()
    {
        ChangeScene.SetActive(true);
        MaskObject.SetActive(false);
        BlackSquare.SetActive(true);
        Color bright = BlackSquare.GetComponent<SpriteRenderer>().color;
        bright.a = 0f;
        BlackSquare.GetComponent<SpriteRenderer>().color = bright;
        StartCoroutine(FadeAndLoadScene());
    }

    private IEnumerator FadeAndLoadScene()
    {
        yield return StartCoroutine(Fade());
        SceneManager.LoadScene("Night");
    }

    private void OnMouseDown()
    {
        if (gameObject.name == "Night Button Circle")
        {
            Day2Night_Circle();
        }
        else if (gameObject.name == "Night Button Bright")
        {
            Day2Night_Bright();
        }
        else if (gameObject.name == "Draw Button")
        {
            CardManager.Inst.draw();
        }
    }
}
