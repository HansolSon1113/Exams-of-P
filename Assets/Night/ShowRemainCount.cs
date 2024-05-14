using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowRemainCount : MonoBehaviour
{
    [SerializeField] ItemSO itemSO;
    [SerializeField] TMP_Text major;
    [SerializeField] TMP_Text liberal;
    [SerializeField] TMP_Text work;
    [SerializeField] TMP_Text play;
    public int majorCount, liberalCount, workCount, playCount;
    void Start()
    {
        setCounts();
        setTexts();
    }

    private void setCounts(){
        for(int i = 0; i < itemSO.items.Length; i++){
            switch(itemSO.items[i].type){
                case 0:
                    majorCount++;
                    break;
                case 1:
                    liberalCount++;
                    break;
                case 2:
                    workCount++;
                    break;
                case 3:
                    playCount++;
                    break;
            }
        }
    }

    private void setTexts(){
        major.text = "X" + majorCount;
        liberal.text = "X" + liberalCount;
        work.text = "X" + workCount;
        play.text = "X" + playCount;
    }
}
