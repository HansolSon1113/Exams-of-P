using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
using Unity.VisualScripting;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;

    public Item item;
    public PRS originPRS;
    private bool isSelected = false;
    private Vector3 originLocation;
    private bool isDragging = false;
    private bool isClicked = false;
    private Vector3 targetLocation;

    public void Setup(Item item)
    {
        this.item = item;
        this.card.sprite = this.item.sprite;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Day")
        {
            GameObject.Find("Clock").GetComponent<Clock>().moveClock(this.item.time);
        }
    }

    public void MoveTransform(PRS prs, float time)
    {
        this.transform.DOMove(prs.pos, time);
        this.transform.DORotateQuaternion(prs.rot, time);
        this.transform.DOScale(prs.scale, time);
    }

    private int nightCardClear()
    {
        for (int i = 0; i < 3; i++)
        {
            if (NightCardManager.Inst.selectedCards[i] != null)
            {
                if (NightCardManager.Inst.selectedCards[i].item.name == this.item.name)
                {
                    NightCardManager.Inst.selectedCards[i] = null;
                    return i;
                }
            }
        }
        return -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (SceneManager.GetActiveScene().name == "Night")
        {
            int thisIndex = nightCardClear();
            Audio.Inst.playCardOnTarget();
            isDragging = false;
            isSelected = true;
            targetLocation = other.transform.position;
            if (other.gameObject.name == "Target 1")
            {
                if (NightCardManager.Inst.selectedCards[0] != null)
                {
                    if (thisIndex != -1)
                    {
                        NightCardManager.Inst.selectedCards[0].transform.DOMove(this.originLocation, 0.1f);
                        NightCardManager.Inst.selectedCards[thisIndex] = NightCardManager.Inst.selectedCards[0];
                        NightCardManager.Inst.isUsed(this);
                        NightCardManager.Inst.selectedCards[0] = this;
                    }
                    else
                    {
                        // NightCardManager.Inst.unUsed(NightCardManager.Inst.selectedCards[0]);
                        // Destroy(NightCardManager.Inst.selectedCards[0].gameObject);
                        isSelected = false;
                        isDragging = false;
                    }
                }
            }
            else if (other.gameObject.name == "Target 2")
            {
                if (NightCardManager.Inst.selectedCards[1] != null)
                {
                    if (thisIndex != -1)
                    {
                        NightCardManager.Inst.selectedCards[1].transform.DOMove(this.originLocation, 0.1f);
                        NightCardManager.Inst.selectedCards[thisIndex] = NightCardManager.Inst.selectedCards[1];
                        NightCardManager.Inst.isUsed(this);
                        NightCardManager.Inst.selectedCards[1] = this;
                    }
                    else
                    {
                        // NightCardManager.Inst.unUsed(NightCardManager.Inst.selectedCards[1]);
                        // Destroy(NightCardManager.Inst.selectedCards[1].gameObject);
                        isSelected = false;
                        isDragging = false;
                    }
                }
            }
            else if (other.gameObject.name == "Target 3")
            {
                if (NightCardManager.Inst.selectedCards[2] != null)
                {
                    if (thisIndex != -1)
                    {
                        NightCardManager.Inst.selectedCards[2].transform.DOMove(this.originLocation, 0.1f);
                        NightCardManager.Inst.selectedCards[thisIndex] = NightCardManager.Inst.selectedCards[2];
                        NightCardManager.Inst.isUsed(this);
                        NightCardManager.Inst.selectedCards[2] = this;
                    }
                    else
                    {
                        // NightCardManager.Inst.unUsed(NightCardManager.Inst.selectedCards[2]);
                        // Destroy(NightCardManager.Inst.selectedCards[2].gameObject);
                        isSelected = false;
                        isDragging = false;
                    }
                }
            }
            NightCardManager.Inst.showCards(NightCardManager.Inst.currentType, true);
        }
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.transform.DOScale(Vector3.one * 0.6f, 0.1f);
        originLocation = targetLocation;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        isClicked = true;
        originLocation = this.transform.position;
    }

    private void OnMouseDrag()
    {
        if (this.item.type != 4)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 objPosition = new Vector2(mousePosition.x, mousePosition.y);
            this.transform.DOMove(objPosition, 0.01f);
            if (isDragging && isSelected && mousePosition.y >= -1.5f)
            {
                isSelected = false;
                isDragging = false;
                nightCardClear();
                NightCardManager.Inst.unUsed(this);
                NightCardManager.Inst.showCards(NightCardManager.Inst.currentType, true);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (isClicked == true)
        {
            this.transform.DOMove(originLocation, 0.3f);
            isClicked = false;
        }
    }
}
