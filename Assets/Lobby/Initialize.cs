using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize : MonoBehaviour
{
    [SerializeField] ItemSO itemSO;

    public void gameStart(){
        for(int i = 0; i < itemSO.items.Length; i++){
            itemSO.items[i].used = false;
            itemSO.items[i].pass = false;
        }
        CostManager.MP = 100;
        CostManager.drawedCardCount = 0;
        CostManager.drawedMajor = 0;
        CostManager.drawedLib = 0;
        CostManager.drawedWork = 0;
        CostManager.drawedPlay = 0;
        SceneManager.LoadScene("Night");
    }
}
