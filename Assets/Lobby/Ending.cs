using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ending : MonoBehaviour
{
    public TMP_Text major;
    public TMP_Text lib;
    public TMP_Text work;
    public TMP_Text play;
    void Start()
    {
        major.text = "전공: " + CostManager.drawedMajor;
        lib.text = "교양: " + CostManager.drawedLib;
        work.text = "알바: " + CostManager.drawedWork;
        play.text = "여가: " + CostManager.drawedPlay;
    }
}
