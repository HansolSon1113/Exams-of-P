using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    List<Item> itemBuffer;
    private int targetCount = 0;
    public GameObject target;

    void Start()
    {
        SetupItemBuffer();
    }

    public void draw(){
        AddCard();
    }

    public void draw5(){
        targetCount++;
        if(targetCount > 7){
            targetCount -= 7;
        }
        for(int i = 0; i < 5; i++)
        {
            AddCard();
        }
        var newTarget = Instantiate(target, new Vector3(-8+2*targetCount, -3, 0), Utils.QI);
    }

    public void clearCards(Card donotDestroy){
        for(int i = 0; i < cards.Count; i++)
        {
            if(cards[i] != donotDestroy)
                Destroy(cards[i].gameObject);
        }
        cards.Clear();
    }

    void SetupItemBuffer()
    {
        itemBuffer = new List<Item>(10);
        for(int i = 0; i < itemSO.items.Length; i++)
        {
            itemBuffer.Add(itemSO.items[i]);
        }
        
        for(int i = 0; i < itemBuffer.Count; i++)
        {
            int randomIndex = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[randomIndex];
            itemBuffer[randomIndex] = temp;
        }
    }

    public Item PopItem()
    {
        if(itemBuffer.Count == 0)
        {
            SetupItemBuffer();
        }

        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item;
    }

    void AddCard()
    {
        var cardObject = Instantiate(cardPrefab, cardManager.transform.position, Utils.QI);
        var card = cardObject.GetComponent<Card>();
        card.Setup(PopItem());
        cards.Add(card);
        SetOriginOrder();
        CardAlignment();
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
        originCardPRSs = RoundAlignment(leftAlignment, rightAlignment, cards.Count, 0.5f, Vector3.one * 1.9f);
        for(int i = 0; i < cards.Count; i++)
        {
            var targetCard = cards[i];
            targetCard.originPRS = originCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, 0.7f);
        }
    }

    public void willDestroyCard(Card card)
    {
        cards.Remove(card);
        SetOriginOrder();
        CardAlignment();
    }

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
