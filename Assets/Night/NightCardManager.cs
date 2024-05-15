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
    public List<Card> selectedCards;
    public int currentShowing;
    [SerializeField] GameObject leftScroller;
    [SerializeField] GameObject rightScroller;

    public bool isLeftScrollEnabled = false;
    public bool isRightScrollEnabled = false;

    void Start(){
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        currentShowing = -1;
    }

    public void draw(int type){
        isLeftScrollEnabled = false;
        isRightScrollEnabled = false;
        if(currentShowing != type)
        {
            clearCards();
            currentShowing = type;
            AddCard(type);
            if(cards.Count > 6){
                isRightScrollEnabled = true;
            }
        }
    }

    private void Update()
    {
        if(NightCardManager.Inst.isLeftScrollEnabled){
            leftScroller.SetActive(true);
        }
        else
        {
            leftScroller.SetActive(false);
        }

        if(NightCardManager.Inst.isRightScrollEnabled){
            rightScroller.SetActive(true);
        }
        else
        {
            rightScroller.SetActive(false);
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
        for (int i = 0; i < cards.Count; i++)
        {
            var targetCard = cards[i];
            Vector3 newPosition = targetCard.originPRS.pos + new Vector3(scrollDelta, 0, 0);
            targetCard.originPRS = new PRS(newPosition, Utils.QI, Vector3.one * 1.9f);
            targetCard.MoveTransform(targetCard.originPRS, 0.1f);
            hidCard(targetCard.originPRS, targetCard);
        }
        if(cards.Count > 6){
            if(cards[cards.Count-1].originPRS.pos.x <= 8){
                isRightScrollEnabled = false;
            }
            else
            {
                isRightScrollEnabled = true;
            }
            if(cards[0].originPRS.pos.x >= -8){
                isLeftScrollEnabled = false;
            }
            else
            {
                isLeftScrollEnabled = true;
            }
        }
        else{
            isLeftScrollEnabled = false;
            isRightScrollEnabled = false;
        }
    }

    //selectedCards 제외하고 삭제
    public void clearCards(){
        foreach (var card in cards)
        {
            if (!selectedCards.Contains(card))
            {
                Destroy(card.gameObject);
            }
        }
        cards.Clear();
    }


    public void END(){
        clearCards();
        for(int i = 0; i < selectedCards.Count; i++)
        {
            Destroy(selectedCards[i].gameObject);
        }
        selectedCards.Clear();
        cards.Clear();
        Destroy(target1);
        Destroy(target2);
        Destroy(target3);
    }

    private void addBlank(){
        for(int i = 0;i < itemSO.items.Length; i++){
            if(itemSO.items[i].type == 4) {
                var cardObject = Instantiate(cardPrefab, cardManager.position, Utils.QI);
                var card = cardObject.GetComponent<Card>();
                card.Setup(itemSO.items[i]);
                cards.Add(card);
                break;
            }
        }
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

    void AddCard(int type)
    {
        for(int i = 0; i < itemSO.items.Length; i++)
        {
            if(itemSO.items[i].type == type && itemSO.items[i].used == false)
            {
                if (cardManager != null && cardManager.position != null)
                {
                    var cardObject = Instantiate(cardPrefab, cardManager.position, Utils.QI);
                    var card = cardObject.GetComponent<Card>();
                    card.Setup(itemSO.items[i]);
                    cards.Add(card);
                }
            }
            else if(itemSO.items[i].type == type && itemSO.items[i].used == true)
            {
                addBlank();
            }
        }
        while(cards.Count%6 != 0){
            addBlank();
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
            alignment = new Vector3(-6 + i * 2.4f, 0, 0);
            targetCard.originPRS = new PRS(alignment, Utils.QI, Vector3.one * 1.9f);
            targetCard.MoveTransform(targetCard.originPRS, 0.1f);
            hidCard(targetCard.originPRS, targetCard);
        }
    }    

    private void hidCard(PRS originPRS, Card targetCard){
        if(targetCard.originPRS.pos.x >= 8 || targetCard.originPRS.pos.x <= -8){
            targetCard.gameObject.SetActive(false);
            }
        else{
            targetCard.gameObject.SetActive(true);
        }
    }
}

