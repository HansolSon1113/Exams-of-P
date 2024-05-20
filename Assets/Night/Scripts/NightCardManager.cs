using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class NightCardManager : MonoBehaviour
{
    public static NightCardManager Inst { get; private set; }
    void Awake() => Inst = this;
    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardManager;
    [SerializeField] List<Card> cards;
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
    public int scrollCount = 0;
    public int currentType = -1;
    public List<Card> selectedCards;
    private List<bool> originUsed = new List<bool>();
    [SerializeField] GameObject leftScroller;
    [SerializeField] GameObject rightScroller;
    public GameObject nightEndPanel;
    public bool isLeftScrollEnabled = false;
    public bool isRightScrollEnabled = false;
    public GameObject MaskObject;
    public GameObject BlackSquare;
    public GameObject ChangeScene;
    [SerializeField] SpriteRenderer overPanel;

    void Start()
    {
        Time.timeScale = 1f;
        Audio.Inst.playNightBackground();
        if (CostManager.dayCount >= 7)
        {
            Audio.Inst.playSceneChange();
            overPanel.DOFade(1f, 1.5f).OnComplete(() =>
            {
                Time.timeScale = 0f;
                SceneManager.LoadScene("Normal Ending");
            });
        }
        for (int i = 0; i < 3; i++)
        {
            selectedCards.Add(null);
        }
        for (int i = 0; i < itemSO.items.Length - 1; i++)
        {
            originUsed.Add(itemSO.items[i].used);
        }
    }

    public void showCards(int type, bool imediate){
        isLeftScrollEnabled = false;
        clearCards();
        AddCard(type, imediate);
        currentType = type;
        if(cards[cards.Count-1].originPRS.pos.x > 8){
            isRightScrollEnabled = true;
        }
        else
        {
            isRightScrollEnabled = false;
        }
    }

    public void Night2Day_Circle()
    {
        ChangeScene.SetActive(true);
        Audio.Inst.playSceneChange();
        MaskObject.transform.DOScale(new Vector3(0, 0, 1), 3f).OnComplete(() =>
        {
            ChangeScene.SetActive(false);
            nightEndPanel.SetActive(true);
        });
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

    public void ScrollCards(float scrollDelta, bool imediate)
    {
        Audio.Inst.playSlide();
        for (int i = 0; i < cards.Count; i++)
        {
            var targetCard = cards[i];
            Vector3 newPosition = targetCard.originPRS.pos + new Vector3(scrollDelta, 0, 0);
            targetCard.originPRS = new PRS(newPosition, Utils.QI, Vector3.one * 0.7f);
            if (imediate)
            {
                targetCard.transform.position = targetCard.originPRS.pos;
            }
            else
            {
                targetCard.MoveTransform(targetCard.originPRS, 0.1f);
            }
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


    //다음 날에 데이터 넘기기
    public void END()
    {
        ChangeScene.SetActive(true);
        int passCount = 0;
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            itemSO.items[i].used = false;
            for (int j = 0; j < 3; j++)
            {
                try
                {
                    if (selectedCards[j] != null)
                    {
                        if (selectedCards[j].item.name == itemSO.items[i].name && selectedCards[j].item.used == false)
                        {
                            CostManager.passedCards.Add(selectedCards[j].item);
                            itemSO.items[i].pass = true;
                            passCount++;
                            Debug.Log(passCount);
                            break;
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
        for (int i = passCount; i < 3; i++)
        {
            var randIndex = Random.Range(0, itemSO.items.Length);
            if (itemSO.items[randIndex].pass == false && itemSO.items[randIndex].used == false)
            {
                CostManager.passedCards.Add(itemSO.items[randIndex]);
                itemSO.items[randIndex].pass = true;
            }
            else
            {
                i--;
            }
        }
        clearCards();
        for (int i = 0; i < 3; i++)
        {
            if (selectedCards[i] != null)
            {
                Destroy(selectedCards[i].gameObject);
            }
        }
        selectedCards.Clear();
        cards.Clear();
        Destroy(target1);
        Destroy(target2);
        Destroy(target3);

        for (int i = 0; i < itemSO.items.Length - 1; i++)
        {
            itemSO.items[i].used = originUsed[i];
        }
    }

    //빈칸 추가
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

    //사용된 카드
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

    //카드 추가(itemSO에 직접 접근)
    public void AddCard(int type, bool imediate)
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
        CardAlignment(imediate);
    }

    //표시 순서 설정
    public void SetOriginOrder()
    {
        int count = cards.Count;
        for(int i = 0; i < count; i++)
        {
            var targetCard = cards[i];
            targetCard?.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    //카드 정렬
    public void CardAlignment(bool imediate)
    {
        for(int i = 0; i < cards.Count; i++)
        {
            var targetCard = cards[i];
            Vector3 alignment;
            alignment = new Vector3(-6.3f + i * 2.45f, 1, 0);
            targetCard.originPRS = new PRS(alignment, Utils.QI, Vector3.one * 0.7f);
            if(imediate)
            {
                targetCard.transform.position = targetCard.originPRS.pos;
                targetCard.transform.localScale = targetCard.originPRS.scale;
            }
            else
            {
                targetCard.MoveTransform(targetCard.originPRS, 0.2f);
            }
            hidCard(targetCard.originPRS, targetCard);
        }
        if(scrollCount != 0)
        {
            ScrollCards(scrollCount * -14.7f, true);
        }
    }    

    //화면 밖 카드 숨기기
    private void hidCard(PRS originPRS, Card targetCard){
        if(targetCard.originPRS.pos.x >= 8 || targetCard.originPRS.pos.x <= -8){
            targetCard.gameObject.SetActive(false);
            }
        else{
            targetCard.gameObject.SetActive(true);
        }
    }
}

