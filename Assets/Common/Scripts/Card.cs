using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if(SceneManager.GetActiveScene().name == "TestScene")
        {
            GameObject.Find("Clock").GetComponent<Clock>().moveClock(this.item.time);
        }
    }

    public void MoveTransform(PRS prs, float time)
    {
        transform.DOMove(prs.pos, time);
        transform.DORotateQuaternion(prs.rot, time);
        transform.DOScale(prs.scale, time);
    }

    private int nightCardClear(){
        for(int i = 0; i < 3; i++)
        {
            if(NightCardManager.Inst.selectedCards[i] != null)
            {
                if(NightCardManager.Inst.selectedCards[i].item.name == this.item.name)
                {
                    NightCardManager.Inst.selectedCards[i] = null;
                    return i;
                }
            }
        }
        return -1;
    }

    /*private void printCardName()
    {
        if(NightCardManager.Inst.selectedCards[0] != null)
            Debug.Log("1" + NightCardManager.Inst.selectedCards[0].item.name);
        if(NightCardManager.Inst.selectedCards[1] != null)
            Debug.Log("2" + NightCardManager.Inst.selectedCards[1].item.name);
        if(NightCardManager.Inst.selectedCards[2] != null)
            Debug.Log("3" + NightCardManager.Inst.selectedCards[2].item.name);
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(SceneManager.GetActiveScene().name == "Night")
        {
            isDragging = false;
            isSelected = true;
            int thisIndex = nightCardClear();
            if (other.gameObject.name == "Target 1")
            {
                if(NightCardManager.Inst.selectedCards[0] != null)
                {
                    if(thisIndex != -1)
                    {
                        NightCardManager.Inst.selectedCards[0].transform.DOMove(this.originLocation, 0.1f);
                        NightCardManager.Inst.selectedCards[thisIndex] = NightCardManager.Inst.selectedCards[0];
                    }
                    else
                    {
                        NightCardManager.Inst.unUsed(NightCardManager.Inst.selectedCards[0]);
                        Destroy(NightCardManager.Inst.selectedCards[0].gameObject);
                    }
                }
                NightCardManager.Inst.selectedCards[0] = this;
            }
            else if (other.gameObject.name == "Target 2")
            {
                if (NightCardManager.Inst.selectedCards[1] != null)
                {
                   if(thisIndex != -1)
                    {
                        NightCardManager.Inst.selectedCards[1].transform.DOMove(this.originLocation, 0.1f);
                        NightCardManager.Inst.selectedCards[thisIndex] = NightCardManager.Inst.selectedCards[1];
                    }
                    else
                    {
                        NightCardManager.Inst.unUsed(NightCardManager.Inst.selectedCards[1]);
                        Destroy(NightCardManager.Inst.selectedCards[1].gameObject);
                    }
                }
                NightCardManager.Inst.selectedCards[1] = this;
            }
            else if (other.gameObject.name == "Target 3")
            {
                if (NightCardManager.Inst.selectedCards[2] != null)
                {
                    if(thisIndex != -1)
                    {
                        NightCardManager.Inst.selectedCards[2].transform.DOMove(this.originLocation, 0.1f);
                        NightCardManager.Inst.selectedCards[thisIndex] = NightCardManager.Inst.selectedCards[2];
                    }
                    else
                    {
                        NightCardManager.Inst.unUsed(NightCardManager.Inst.selectedCards[2]);
                        Destroy(NightCardManager.Inst.selectedCards[2].gameObject);
                    }
                }
                NightCardManager.Inst.selectedCards[2] = this;
            }
            NightCardManager.Inst.clearCards();
            NightCardManager.Inst.currentShowing = -1;
            NightCardManager.Inst.isUsed(this);
            NightCardManager.Inst.isLeftScrollEnabled = false;
            NightCardManager.Inst.isRightScrollEnabled = false;
        }
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.transform.DOMove(other.transform.position, 0.05f);
    }

    private void OnMouseDown()
    {
        isDragging = true;
        originLocation = this.transform.position;
    }

    private void OnMouseDrag()
    {
        if(isDragging && this.item.type != 4)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 objPosition = new Vector2(mousePosition.x, mousePosition.y);
            this.transform.DOMove(objPosition, 0.01f);
            if(isSelected && mousePosition.y >= -1.5f)
            {
                isSelected = false;
                isDragging = false;
                nightCardClear();
                NightCardManager.Inst.unUsed(this);
                Destroy(this.gameObject);
            }
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
