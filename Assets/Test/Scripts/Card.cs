using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;
    [SerializeField] TMP_Text nameText;

    public Item item;
    public PRS originPRS;

    public void Setup(Item item)
    {
        this.item = item;
        this.card.sprite = this.item.sprite;
        nameText.text = this.item.name;
    }

    public void MoveTransform(PRS prs, float time)
    {
        transform.DOMove(prs.pos, time);
        transform.DORotateQuaternion(prs.rot, time);
        transform.DOScale(prs.scale, time);
    }

    private void OnTriggerEnter2D(Collider2D other){
        GameObject.Find("Clock").GetComponent<Clock>().moveClock(this.item.time);
        GameObject.Find("Card Manager").GetComponent<CardManager>().willDestroyCard(this);
        Destroy(this.gameObject);
    }
}
