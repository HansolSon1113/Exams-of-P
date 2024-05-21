using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireEnding : MonoBehaviour
{
    [SerializeField] SpriteRenderer overPanel;
    void Start()
    {
        Time.timeScale = 1f;
        overPanel.DOFade(0f, 1.5f);
        Audio.Inst.playFireEnding();
    }
}
