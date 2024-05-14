using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private void OnMouseDown(){
        float scrollSpeed;
        if(this.gameObject.name == "Scroll Left"){
            scrollSpeed = 2f;
        }
        else{
            scrollSpeed = -2f;
        }
        NightCardManager.Inst.ScrollCards(scrollSpeed);
    }
}
