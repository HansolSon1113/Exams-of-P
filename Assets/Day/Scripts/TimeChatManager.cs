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
        chatBox.transform.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.6f).SetEase(Ease.OutBounce);
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
        float leftTime = 24f - time;
        List<string> str = new List<string>();
        if(time <= 11f)
        {
            str.Add(CardManager.Inst.usedTime + "시간 밖에 안 됐는데..\n왜 물어보는거야?");
            str.Add("이제 겨우\n" + time + "시 밖에 지나지 않았다고");
            str.Add("그만 봐\n" + leftTime + "시간 남았다고");
            str.Add("째깍 째깍\n" + leftTime + "시간 남았습니다~~");
        }
        else if (time >= 12 && time <= 17)
        {
            str.Add("힘들어 하지마?\n이제 겨우 " + CardManager.Inst.usedTime + "시간이야");
            str.Add("너무 힘들어?\n아쉽게도 지금 " + CardManager.Inst.usedTime + "시간이야");
            str.Add("얼마 남았을까요??\n땡! " + leftTime + "시간입니다");
            str.Add("째깍 째깍\n" + leftTime + "시간 남았습니다~~");
        }
        else
        {
            str.Add("이제\n" + leftTime + "시간 밖에 남지 않았다고");
            str.Add("뭐하는 거야!!\n" + CardManager.Inst.usedTime + "시간이라고");
            str.Add("이제 그만해!!\n이제 " + CardManager.Inst.usedTime + "시간이야");
            str.Add("감당 가능하겠어?\n" + CardManager.Inst.usedTime + "시간이야");
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
        chatSprite.size += new Vector2(width + 0.5f, 0);
    }
}
