using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TimeChatManager : MonoBehaviour
{
    [SerializeField] TMP_Text chatText;
    [SerializeField] GameObject chatBox;
    [SerializeField] SpriteRenderer chatSprite;
    private void Start()
    {
        chatText.text = setString();
        if (ShowMP.Inst.isShowing)
        {
            chatBox?.GetComponent<Order>().SetOriginOrder(CostManager.chatOrder);
            CostManager.chatOrder += 10;
        }
        else
        {
            CostManager.chatOrder = 110;
            chatBox?.GetComponent<Order>().SetOriginOrder(100);
        }
        chatBox.transform.DOScale(new Vector3(0.3f, 0.3f, 1f), 0.6f).SetEase(Ease.OutBounce);
        chatBox.transform.DORotate(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.OutBack);
        Invoke("destroy", 2f);
    }

    private void destroy()
    {
        Clock.Inst.isShowing = false;
        Destroy(chatBox);
    }

    private string setString()
    {
        float time = CardManager.Inst.usedTime + CostManager.startTime;
        float scriptTime = time;
        if (scriptTime >= 24)
        {
            scriptTime -= 24;
        }
        List<string> str = new List<string>();
        if (!CostManager.isTimeChatUsed)
        {
            str.Add("지금은 " + scriptTime + "시야.\n내가 아니면 안되겠구만.");
        }
        else
        {
            if (time <= 11f)
            {
                str.Add("...시계 숫자도 못 읽는거야?\n흥, " + scriptTime + "시라고.");
                str.Add("성실한거야? 멍청한거야?\n" + scriptTime + "시라고 말만 해둘게.");
                str.Add("몇 번을 말해줘야 해? " + scriptTime + "시라고!");
            }
            else if (time >= 12 && time <= 17)
            {
                str.Add("와~ 꼴이 엉망진창인데?\n벌써 시간이 " + scriptTime + "신데...");
                str.Add("야! 시끄러워! 몇 신지 알아?\n" + scriptTime + "시라고!");
                str.Add(scriptTime + "시라는 시간을 보고도 웃음이 나오나보네.");
                str.Add(scriptTime + "시.\n이렇게 말해주는 것도 시간낭비야.");
            }
            else if (time >= 18)
            {
                str.Add(scriptTime + "시...\n흥, 시간 관리 못하는 사람이랑은 얘기하고 싶지 않은데~");
                str.Add("내 시계는 정확해.\n" + scriptTime + "시야.");
                str.Add("시간은 금보다 귀해. 넌 이해 못하겠지만.\n" + scriptTime + "시라는 걸 알아둬.");
                str.Add("벌써 " + scriptTime + "시네~\n시간낭비는 너의 특기인가봐?");
            }
        }
        string res = str[Random.Range(0, str.Count)];
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
        chatSprite.size += new Vector2(width + 0.3f, 0);
    }
}
