using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackClicked : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (this.gameObject.name == "Major Pack")
        {
            NightCardManager.Inst.showCards(NightCardManager.MAJOR);
        }
        else if (this.gameObject.name == "Liberal Pack")
        {
            NightCardManager.Inst.showCards(NightCardManager.LIB);
        }
        else if (this.gameObject.name == "Work Pack")
        {
            NightCardManager.Inst.showCards(NightCardManager.WORK);
        }
        else if (this.gameObject.name == "Play Pack")
        {
            NightCardManager.Inst.showCards(NightCardManager.PLAY);
        }
    }
}
