using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackClicked : MonoBehaviour
{
    private void OnMouseDown()
    {
        NightCardManager.Inst.scrollCount = 0;
        if (this.gameObject.name == "Major Pack")
        {
            NightCardManager.Inst.showCards(NightCardManager.MAJOR, false);
        }
        else if (this.gameObject.name == "Liberal Pack")
        {
            NightCardManager.Inst.showCards(NightCardManager.LIB, false);
        }
        else if (this.gameObject.name == "Work Pack")
        {
            NightCardManager.Inst.showCards(NightCardManager.WORK, false);
        }
        else if (this.gameObject.name == "Play Pack")
        {
            NightCardManager.Inst.showCards(NightCardManager.PLAY, false);
        }
    }
}
