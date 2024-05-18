using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private void OnMouseDown(){
        float scrollSpeed;
        if(this.gameObject.name == "Scroll Left"){
            scrollSpeed = 15f;
            NightCardManager.Inst.scrollCount--;
        }
        else{
            scrollSpeed = -15f;
            NightCardManager.Inst.scrollCount++;
        }
        NightCardManager.Inst.ScrollCards(scrollSpeed, false);
    }
}
