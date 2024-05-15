using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize : MonoBehaviour
{
    [SerializeField] ItemSO itemSO;

    private void Start(){
        for(int i = 0; i < itemSO.items.Length; i++){
            itemSO.items[i].used = false;
        }
        SceneManager.LoadScene("Night");
    }
}
