using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private void OnMouseDown(){
        float scrollSpeed;
        if(this.gameObject.name == "Scroll Left"){
            scrollSpeed = 14.4f;
        }
        else{
            scrollSpeed = -14.4f;
        }
        NightCardManager.Inst.ScrollCards(scrollSpeed);
    }
}
