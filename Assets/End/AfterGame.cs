using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterGame : MonoBehaviour
{
    [SerializeField] ItemSO itemSO;

    private void OnMouseDown() {
        if (gameObject.name == "Title Button") {
            SceneManager.LoadScene("Lobby");
            // 애니메이션 추가
        }
        if (gameObject.name == "Restart Button") {
            // 애니메이션 추가
            gameStart();
        }
    }
        public void gameStart(){
        for(int i = 0; i < itemSO.items.Length; i++){
            itemSO.items[i].used = false;
            itemSO.items[i].pass = false;
        }
        CostManager.passedCards = new List<Item>();
        CostManager.MP = 100;
        CostManager.drawedCardCount = 0;
        CostManager.drawedMajor = 0;
        CostManager.drawedLib = 0;
        CostManager.drawedWork = 0;
        CostManager.drawedPlay = 0;
        CostManager.dayCount = 0;
        CostManager.startTime = 6f;
        SceneManager.LoadScene("Night");
    }
}
