using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackClicked : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (this.gameObject.name == "Major Pack")
        {
            NightCardManager.Inst.draw(NightCardManager.MAJOR);
        }
        else if (this.gameObject.name == "Liberal Pack")
        {
            NightCardManager.Inst.draw(NightCardManager.LIB);
        }
        else if (this.gameObject.name == "Work Pack")
        {
            NightCardManager.Inst.draw(NightCardManager.WORK);
        }
        else if (this.gameObject.name == "Play Pack")
        {
            NightCardManager.Inst.draw(NightCardManager.PLAY);
        }
    }
}
