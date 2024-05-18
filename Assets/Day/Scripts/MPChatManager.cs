using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MPChatManager : MonoBehaviour
{
    [SerializeField] TMP_Text chatText;
    [SerializeField] GameObject chatBox;
    [SerializeField] SpriteRenderer chatSprite;
    private void Start()
    {
        chatText.text = setString();
        if(Clock.Inst.isShowing)
        {
            chatBox?.GetComponent<Order>().SetOriginOrder(CostManager.chatOrder);
            CostManager.chatOrder += 10;
        }
        else
        {
            CostManager.chatOrder = 110;
            chatBox?.GetComponent<Order>().SetOriginOrder(100);
        }
        chatBox.transform.DOScale(new Vector3(0.4f, 0.4f, 1f), 0.6f).SetEase(Ease.OutBounce);
        chatBox.transform.DORotate(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.OutBack);
        Invoke("destroy", 2f);
    }

    private void destroy()
    {
        ShowMP.Inst.isShowing = false;
        Destroy(chatBox);
    }

    private string setString()
    {
        int MP = CostManager.MP;
        List<string> str = new List<string>();
        if (MP >= 66)
        {
            str.Add(MP + "이야!! " + MP + "이야!!");
            str.Add(MP + "이여서 나도 행복해!!");
            str.Add("좋아!! 지금 " + MP + "이야!!");
            str.Add("너무 좋아 보여!! " + MP + "이야!!");
        }
        else if (MP >= 33 && MP <= 65)
        {
            str.Add(MP + "이야!! " + MP + "이야!!");
            str.Add("주인님 힘들어 보여!! " + MP + "이야!!");
            str.Add("조금만 쉬어!! " + MP + "이야!!");
            str.Add("아슬아슬해 보여!! " + MP + "이야!!");
        }
        else
        {
            str.Add(MP + "이야!! " + MP + "이야!!");
            str.Add("조심해!! " + MP + "이야!!");
            str.Add("쓰러지겠어!! " + MP + "이야!!");
            str.Add("빨리 쉬어!! " + MP + "이야!!");
        }

        string res = str[Random.Range(0, 4)];
        if (!string.IsNullOrWhiteSpace(res))
        {
            string[] lines = res.Split('\n');
            int maxLength = 0;
            foreach (string line in lines)
            {
                int lineLength = line.Length;
                foreach (char c in line)
                {
                    if (c == '!' || c == '.')
                    {
                        lineLength--;
                    }
                }
                if (lineLength > maxLength)
                {
                    maxLength = lineLength;
                }
            }
            setBox(maxLength - 6);
        }

        return res;
    }

    private void setBox(int width)
    {
        chatSprite.size += new Vector2(width+1f, 0);
    }
}
