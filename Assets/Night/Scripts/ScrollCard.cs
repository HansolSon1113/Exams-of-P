using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private void OnMouseDown(){
        float scrollSpeed;
        if(this.gameObject.name == "Scroll Left"){
            scrollSpeed = 14.7f;
            NightCardManager.Inst.scrollCount--;
        }
        else{
            scrollSpeed = -14.7f;
            NightCardManager.Inst.scrollCount++;
        }
        NightCardManager.Inst.ScrollCards(scrollSpeed, false);
    }
}
