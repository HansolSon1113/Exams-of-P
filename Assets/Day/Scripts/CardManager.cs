using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject endPanel;
    [SerializeField] GameObject MP;
    [SerializeField] GameObject burstPanel;
    public GameObject target;
    private int passCount = 0;
    private int cardCount = 0;
    public float usedTime;

    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        MP.transform.localScale = new Vector3(CostManager.MP/10f, 0.5f, 1);
        if(MP.transform.localScale.x <= 0f)
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("Ending");
        }
    }

    public void draw(){
        if(cardCount == 7 || usedTime > 24f - CostManager.startTime)
        {
            clearCards(null);
            burstPanel.SetActive(false);
            END();
            clearCards(null);
            endPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else{
            AddCard();
        }
    }

    //donotDestroy(카드)를 제외한 모든 카드 삭제
    public void clearCards(Card donotDestroy){
        for(int i = 0; i < cards.Count; i++)
        {
            if(cards[i] != donotDestroy)
                Destroy(cards[i].gameObject);
        }
        cards.Clear();
    }

    public void isUsed(Card card)
    {
        for(int i = 0; i < itemSO.items.Length; i++)
        {
            if(card.item.name == itemSO.items[i].name)
            {
                itemSO.items[i].used = true;
            }
        }
    }

    public void unUsed(Card card)
    {
        for(int i = 0; i < itemSO.items.Length; i++)
        {
            if(card.item.name == itemSO.items[i].name)
            {
                itemSO.items[i].used = false;
            }
        }
    }

    void AddCard()
    {
        if(CostManager.drawedCardCount < itemSO.items.Length-1)
        {
            int randIndex;
            do{
                randIndex = Random.Range(0, itemSO.items.Length-1);
            } while(itemSO.items[randIndex].used == true && itemSO.items[randIndex].type != 4);
            if(itemSO.items[randIndex].pass == true)
            {
                passCount++;
            }
            cardCount++;

            if((cardCount == 5 && passCount < 1 || cardCount == 6 && passCount < 2 || cardCount == 7 && passCount < 3))
            {
                do{
                    randIndex = Random.Range(0, itemSO.items.Length-1);
                } while(!CostManager.passedCards.Contains(itemSO.items[randIndex]));
                passCount++;
            }
            if(usedTime <= 24f - CostManager.startTime)
            {
                usedTime += itemSO.items[randIndex].time;
                var cardObject = Instantiate(cardPrefab, cardManager.position, Utils.QI);
                var card = cardObject.GetComponent<Card>();
                card.Setup(itemSO.items[randIndex]);
                cards.Add(card);
                isUsed(card);
                if(usedTime > 24f - CostManager.startTime)
                {
                    burstPanel.SetActive(true);
                    unUsed(card);
                }

                SetOriginOrder();
                CardAlignment();
            
                switch(itemSO.items[randIndex].type)
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
                Debug.Log(CostManager.drawedCardCount);
            }
            if(CostManager.MP - itemSO.items[randIndex].cost <= 100)
                CostManager.MP -= itemSO.items[randIndex].cost * itemSO.items[randIndex].time;
            else
                CostManager.MP = 100;
            MP.transform.localScale = new Vector3(CostManager.MP/10f, 0.5f, 1f);
        }
        else
        {
            END();
            clearCards(null);
            Time.timeScale = 0f;
            SceneManager.LoadScene("Ending");
        }
    }

    public void END()
    {
        CostManager.passedCards.Clear();
        for(int i = 0; i < itemSO.items.Length - 1; i++)
        {
            itemSO.items[i].pass = false;
        }
        if(usedTime <= 24f - CostManager.startTime)
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
        for(int i = 0; i < count; i++)
        {
            var targetCard = cards[i];
            targetCard?.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    void CardAlignment()
    {
        List<PRS> originCardPRSs = new List<PRS>();
        originCardPRSs = RoundAlignment(leftAlignment, rightAlignment, cards.Count, 0.5f, Vector3.one * 1f);
        for(int i = 0; i < cards.Count; i++)
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

        switch(objCount)
        {
            case 1:
                objLerps = new float[] {0.5f};
                break;
            case 2:
                objLerps = new float[] {0.27f, 0.73f};
                break;
            case 3:
                objLerps = new float[] {0.1f, 0.5f, 0.9f};
                break;
            default:
                float interval = 1f / (objCount - 1);
                for(int i = 0; i < objCount; i++)
                {
                    objLerps[i] = interval * i;
                }
                break;
        }

        for(int i = 0; i < objCount; i++){
            var targetPos = Vector3.Lerp(leftAlignment.position, rightAlignment.position, objLerps[i]);
            var targetRot = Utils.QI;
            if(objCount >= 4)
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
}