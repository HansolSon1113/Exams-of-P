using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCardManager : MonoBehaviour
{
    public static NightCardManager Inst { get; private set; }
    void Awake() => Inst = this;
    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardManager;
    [SerializeField] List<Card> cards;
    List<Item> itemBuffer;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject majorPack;
    public GameObject liberalPack;
    public GameObject workPack;
    public GameObject playPack;
    public const int MAJOR = 0;
    public const int LIB = 1;
    public const int WORK = 2;
    public const int PLAY = 3;
    private float scrollSpeed = 0.1f;
    public List<Card> selectedCards;
    public int currentShowing;

    void Start(){
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        currentShowing = -1;
    }

    public void draw(int type){
        if(currentShowing != type)
        {
            clearCards();
            currentShowing = type;
            AddCard(type);
        }
    }

    /*void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (worldMousePosition.x <= -6 || worldMousePosition.x >= 6)
        {
            float scrollDelta = Input.GetAxis("Mouse X") * scrollSpeed * -1;
            ScrollCards(scrollDelta);
        }
    }*/

    public void ScrollCards(float scrollDelta)
    {
        if(cards[0].originPRS.pos.x + scrollDelta > 6 || cards[cards.Count - 1].originPRS.pos.x + scrollDelta < -6)
            return;
        for (int i = 0; i < cards.Count; i++)
        {
            var targetCard = cards[i];
            Vector3 newPosition = targetCard.originPRS.pos + new Vector3(scrollDelta, 0, 0);
            targetCard.originPRS = new PRS(newPosition, Utils.QI, Vector3.one * 1.9f);
            targetCard.MoveTransform(targetCard.originPRS, 0.1f);
        }

    }

    //selectedCards 제외하고 삭제
    public void clearCards(){
        bool isSel = false;
        for(int i = 0; i < cards.Count; i++)
        {
            for(int j = 0; j < selectedCards.Count; j++)
            {
                if(cards[i] == selectedCards[j])
                    isSel = true;
            }
            if(isSel == false)
            {
                Destroy(cards[i].gameObject);
            }
            isSel = false;
        }
        cards.Clear();
    }

    void AddCard(int type)
    {
        for(int i = 0; i < itemSO.items.Length; i++)
        {
            if(itemSO.items[i].type == type)
            {
                if (cardManager != null && cardManager.position != null)
                {
                    var cardObject = Instantiate(cardPrefab, cardManager.position, Utils.QI);
                    var card = cardObject.GetComponent<Card>();
                    card.Setup(itemSO.items[i]);
                    cards.Add(card);
                }
            }
        }
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
        for(int i = 0; i < cards.Count; i++)
        {
            var targetCard = cards[i];
            Vector3 alignment;
            if(cards.Count == 1){
                alignment = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0);
            } 
            else if(cards.Count == 2){
                alignment = Vector3.Lerp(new Vector3(-2, 0, 0), new Vector3(2, 0, 0), (float)i / (cards.Count - 1));
            } 
            else if(cards.Count == 3){
                alignment = Vector3.Lerp(new Vector3(-4, 0, 0), new Vector3(4, 0, 0), (float)i / (cards.Count - 1));
            }
            else if(cards.Count == 4){
                alignment = Vector3.Lerp(new Vector3(-6, 0, 0), new Vector3(6, 0, 0), (float)i / (cards.Count - 1));
            }
            else{
                alignment = Vector3.Lerp(new Vector3(-6, 0, 0), new Vector3(10, 0, 0), (float)i / 4);
            }
            targetCard.originPRS = new PRS(alignment, Utils.QI, Vector3.one * 1.9f);
            targetCard.MoveTransform(targetCard.originPRS, 0.1f);
        }
    }
}

