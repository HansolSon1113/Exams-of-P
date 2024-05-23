using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardManager;
    [SerializeField] List<Card> cards;
    [SerializeField] Transform leftAlignment;
    [SerializeField] Transform rightAlignment;
    [SerializeField] GameObject BlackSqaure;
    [SerializeField] GameObject MaskObject;
    [SerializeField] GameObject drawButton;
    public GameObject burstPanel;
    public GameObject burnoutPanel;
    public GameObject target;
    public GameObject timeCharacter1;
    public GameObject timeCharacter2;
    private int passCount = 0;
    private int cardCount = 0;
    public bool isLoaded = false;
    public float usedTime;
    private List<Item> drawList = new List<Item>();

    void Start()
    {
        CostManager.isMPChatUsed = false;
        CostManager.isTimeChatUsed = false;
        usedTime = 0;
        Audio.Inst.playDayBackground();
        StartCoroutine(chatDelay(2f));

        if (CostManager.MP <= 0)
        {
            burstPanel.SetActive(false);
            Day2NightCircle();
            return;
        }

        int pass = 0;
        for(int i = 0; i < itemSO.items.Length - 1; i++)
        {
            if (itemSO.items[i].pass == true)
            {
                pass++;
            }
        }

        while (drawList.Count < 7 && passCount < 3)
        {
            int randIndex;
            do
            {
                randIndex = Random.Range(0, itemSO.items.Length - 1);
            } while (itemSO.items[randIndex].used == true && itemSO.items[randIndex].type != 4 || drawList.Contains(itemSO.items[randIndex]));
            if (itemSO.items[randIndex].pass == true)
            {
                passCount++;
            }
            if (((drawList.Count == 4 && passCount < 1) && pass >= 3) || ((drawList.Count == 5 && passCount < 2) & pass >= 2) || ((drawList.Count == 6 && passCount < 3) && pass >= 1))
            {
                do
                {
                    randIndex = Random.Range(0, itemSO.items.Length - 1);
                } while (!CostManager.passedCards.Contains(itemSO.items[randIndex]) || drawList.Contains(itemSO.items[randIndex]));
                passCount++;
                pass--;
            }
            drawList.Add(itemSO.items[randIndex]);
        }
        for(int i = 0; i < 7; i++)
        {
            Debug.Log(drawList[i].name);
        }
    }

    private IEnumerator chatDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Clock.Inst.showChatBox();
        yield return new WaitForSeconds(delay);
        ShowMP.Inst.showMPChat();
    }

    public void draw()
    {
        if (cardCount <= 7 && usedTime <= 24f - CostManager.startTime && CostManager.drawedCardCount <= itemSO.items.Length)
        {
            AddCard();
        }
        else
        {
            END();
            Day2NightCircle();
        }
    }

    public void Day2NightCircle()
    {
        END();
        BlackSqaure.SetActive(true);
        MaskObject.SetActive(true);
        Audio.Inst.playSceneChange();
        MaskObject.transform.DOScale(new Vector3(0, 0, 1), 1f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            SceneManager.LoadScene("Night");
        });
    }

    //donotDestroy(카드)를 제외한 모든 카드 삭제
    public void clearCards(Card donotDestroy)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != donotDestroy)
                Destroy(cards[i].gameObject);
        }
        cards.Clear();
    }

    public void isUsed(Card card)
    {
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            if (card.item.name == itemSO.items[i].name)
            {
                itemSO.items[i].used = true;
            }
        }
    }

    public void unUsed(Card card)
    {
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            if (card.item.name == itemSO.items[i].name)
            {
                itemSO.items[i].used = false;
            }
        }
    }


    void AddCard()
    {
        cardCount++;
        int randIndex = Random.Range(0, drawList.Count);
        // 버스트 관련 수정 예정
        if (usedTime <= 24f - CostManager.startTime)
        {
            usedTime += drawList[randIndex].time;
            var cardObject = Instantiate(cardPrefab, cardManager.position, Utils.QI);
            var card = cardObject.GetComponent<Card>();
            card.Setup(drawList[randIndex]);
            cards.Add(card);
            SetOriginOrder();
            CardAlignment();
            isUsed(card);
            if (CostManager.MP - drawList[randIndex].cost * drawList[randIndex].time <= 100)
                CostManager.MP -= drawList[randIndex].cost * drawList[randIndex].time;
            else
                CostManager.MP = 100;
            if (CostManager.MP <= 0)
            {
                clearCards(null);
                burnoutPanel.SetActive(true);
                burnoutPanel.transform.DOScale(new Vector3(0.8f, 0.8f, 1f), 3f).SetEase(Ease.OutBounce);
                burnoutPanel.transform.DOMove(new Vector3(0, 0, 0), 3f).SetEase(Ease.OutBounce).OnComplete(() =>
                {
                    burnoutPanel.SetActive(false);
                    END();
                    BlackSqaure.SetActive(true);
                    MaskObject.SetActive(true);
                    Audio.Inst.playSceneChange();
                    MaskObject.transform.DOScale(new Vector3(0, 0, 1), 1f).SetEase(Ease.OutQuart).OnComplete(() =>
                    {
                        SceneManager.LoadScene("Normal Ending");
                    });
                    return;
                });
            }
            switch (drawList[randIndex].type)
            {
                case 0:
                    CostManager.drawedMajor++;
                    break;
                case 1:
                    CostManager.drawedLib++;
                    break;
                case 2:
                    CostManager.drawedWork++;
                    break;
                case 3:
                    CostManager.drawedPlay++;
                    break;
            }
            CostManager.drawedCardCount++;
            if (usedTime > 24f - CostManager.startTime)
            {
                timeCharacter1.SetActive(false);
                timeCharacter2.SetActive(true);
                Audio.Inst.playBurst();
                clearCards(null);
                drawButton.SetActive(false);
                burstPanel.SetActive(true);
                burstPanel.transform.DOScale(new Vector3(0.8f, 0.8f, 1f), 2f).SetEase(Ease.OutBounce);
                burstPanel.transform.DOMove(new Vector3(0, 0, 0), 2f).SetEase(Ease.OutBounce).OnComplete(() =>
                {
                    CostManager.drawedCardCount--;
                    switch (drawList[randIndex].type)
                    {
                        case 0:
                            CostManager.drawedMajor--;
                            break;
                        case 1:
                            CostManager.drawedLib--;
                            break;
                        case 2:
                            CostManager.drawedWork--;
                            break;
                        case 3:
                            CostManager.drawedPlay--;
                            break;
                    }
                    burstPanel.SetActive(false);
                    unUsed(card);
                    Day2NightCircle();
                });
            }
        }
        drawList.RemoveAt(randIndex);
    }

    public void END()
    {
        CostManager.passedCards.Clear();
        for(int i = 0; i < 3; i++)
        {
            CostManager.passedCards.Add(null);
        }
        for (int i = 0; i < itemSO.items.Length - 1; i++)
        {
            itemSO.items[i].pass = false;
        }
        if (usedTime <= 24f - CostManager.startTime)
        {
            CostManager.startTime = 6f;
        }
        else
        {
            CostManager.startTime = 8f;
        }
    }

    public void SetOriginOrder()
    {
        int count = cards.Count;
        for (int i = 0; i < count; i++)
        {
            var targetCard = cards[i];
            targetCard?.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    void CardAlignment()
    {
        List<PRS> originCardPRSs = new List<PRS>();
        originCardPRSs = RoundAlignment(leftAlignment, rightAlignment, cards.Count, 0.5f, Vector3.one * 0.7f);
        for (int i = 0; i < cards.Count; i++)
        {
            var targetCard = cards[i];
            targetCard.originPRS = originCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, 0.7f);
        }
    }

    //카드 한개 삭제
    /*
    public void willDestroyCard(Card card)
    {
        cards.Remove(card);
        SetOriginOrder();
        CardAlignment();
    }
    */

    List<PRS> RoundAlignment(Transform leftAlignment, Transform rightAlignment, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        switch (objCount)
        {
            case 1:
                objLerps = new float[] { 0.5f };
                break;
            case 2:
                objLerps = new float[] { 0.27f, 0.73f };
                break;
            case 3:
                objLerps = new float[] { 0.1f, 0.5f, 0.9f };
                break;
            default:
                float interval = 1f / (objCount - 1);
                for (int i = 0; i < objCount; i++)
                {
                    objLerps[i] = interval * i;
                }
                break;
        }

        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftAlignment.position, rightAlignment.position, objLerps[i]);
            var targetRot = Utils.QI;
            if (objCount >= 4)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftAlignment.rotation, rightAlignment.rotation, objLerps[i]);
            }
            results.Add(new PRS(targetPos, targetRot, scale));
        }
        return results;
    }

    #region card

    public void CardMouseOver(Card card)
    {
        if (card.item.type != 4)
        {
            EnlargeCard(true, card);
        }
    }

    public void CardMouseExit(Card card)
    {
        if (card.item.type != 4)
        {
            EnlargeCard(false, card);
        }
    }


    void EnlargeCard(bool isEnlarge, Card card)
    {
        if (isEnlarge == true)
        {
            Vector3 enlargePos = new Vector3(card.transform.position.x, -1.8f, -100f);
            card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * 2f), 0);
        }
        else
        {
            //card.MoveTransform(card.originPRS, 0);
        }
        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge);
    }
    #endregion
}