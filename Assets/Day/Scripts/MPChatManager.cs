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
        if (Clock.Inst.isShowing)
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
        chatBox.transform.DORotate(new Vector3(0, 0, 0), 0.4f).SetEase(Ease.OutBack);
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
        if (!CostManager.isMPChatUsed)
        {
            str.Add("전류 공급... 부팅 시작...\n현재 정신력 수치 " + CostManager.MP + ".");
        }
        else
        {
            if (MP >= 66)
            {
                str.Add("정신력 수치 분석 중.\n정신력 수치 " + MP + "(으)로 높음.\n현재 상태 유지 권장.");
                str.Add("시스템 점검 중...\n검사 결과 " + MP + "(으)로 정확.");
                str.Add("수치 " + MP + ". 상태 매우 양호.\n사용자의 상태가 매우 안정적. \n현 상태 유지 바람.");
                str.Add("사용자의 허리 상태 : 양호.\n사용자의 정신력 수치 : " + MP + ".");
            }
            else if (MP >= 33 && MP <= 65)
            {
                str.Add("정신력 수치 분석 중.\n수치 " + MP + "(으)로 스트레스 수치 상승 감지.\n휴식 및 심리적 안정 필요.");
                str.Add("현재 상태 분석 중.\n분석 결과 " + MP + "(으)로 정상.\n충분한 수면 권장.");
                str.Add("정신력 수치가 " + MP + "(으)로 위험도 상승 중.\n비상 메뉴얼 대기 중.");
                str.Add("걱정 감지. 걱정 감지.\n정신력 수치 " + MP + "(으)로 업무 진행 가능한 상태.\n긍정적인으로 접근하길 권장.");
            }
            else
            {
                str.Add("분석 결과 : " + MP + ".\n과로 상태 유지 중.\n과로 상태 유지 중.");
                str.Add(MP + "! " + MP + "!\n사용자의 정신력 수치 " + MP + "!\n감정 조절 바람.");
                str.Add("정신력 수치 " + MP + "(으)로 비상!\n 사용자의 휴식을 요청.");
            }
        }
        string res = str[Random.Range(0, str.Count)];
        string[] lines = res.Split('\n');
        int maxLength = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            int lineLength = line.Length;
            foreach (char c in line)
            {
                if (c == '!')
                {
                    lineLength--;
                }
            }
            if (lineLength > maxLength)
            {
                maxLength = lineLength;
            }
        }
        setBox(maxLength - 6, lines.Length-2);
        return res;
    }

    private void setBox(int width, int height)
    {
        chatSprite.size += new Vector2(width + 0.85f, height + 0.7f);
    }
}
