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
    private bool isSelected = false;
    private Vector3 originLocation;
    private bool isDragging = false;

    public void Setup(Item item)
    {
        this.item = item;
        this.card.sprite = this.item.sprite;
        nameText.text = this.item.name;
    }

    private void Start()
    {
        //GameObject.Find("Clock").GetComponent<Clock>().moveClock(this.item.time);
    }

    public void MoveTransform(PRS prs, float time)
    {
        transform.DOMove(prs.pos, time);
        transform.DORotateQuaternion(prs.rot, time);
        transform.DOScale(prs.scale, time);
    }

    private void OnTriggerEnter2D(Collider2D other){
        isDragging = false;
        isSelected = true;
        NightCardManager.Inst.selectedCards.Add(this);
        NightCardManager.Inst.clearCards();
        NightCardManager.Inst.currentShowing = -1;
        NightCardManager.Inst.isUsed(this);
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.transform.DOMove(other.transform.position, 0.05f);
        Destroy(other.gameObject);
    }

    private void OnMouseDown()
    {
        if(isSelected == false)
        {
            isDragging = true;
            originLocation = this.transform.position;
        }
    }

    private void OnMouseDrag()
    {
        if(isDragging && this.item.type != 4)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 objPosition = new Vector2(mousePosition.x, mousePosition.y);
            this.transform.DOMove(objPosition, 0.01f);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (isSelected == false)
        {
            this.transform.DOMove(originLocation, 0.3f);
        }
    }
}
