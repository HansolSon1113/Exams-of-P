using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dday : MonoBehaviour
{
    [SerializeField] Sprite Day7;
    [SerializeField] Sprite Day6;
    [SerializeField] Sprite Day5;
    [SerializeField] Sprite Day4;
    [SerializeField] Sprite Day3;
    [SerializeField] Sprite Day2;
    [SerializeField] Sprite Day1;
    [SerializeField] Sprite DDay;

    void Start(){
        print(CostManager.dayCount);
        if (CostManager.dayCount == 1)
            this.GetComponent<SpriteRenderer>().sprite = Day7;
        else if (CostManager.dayCount == 2)
            this.GetComponent<SpriteRenderer>().sprite = Day6;
        else if (CostManager.dayCount == 3)
            this.GetComponent<SpriteRenderer>().sprite = Day5;
        else if (CostManager.dayCount == 4)
            this.GetComponent<SpriteRenderer>().sprite = Day4;
        else if (CostManager.dayCount == 5)
            this.GetComponent<SpriteRenderer>().sprite = Day3;
        else if (CostManager.dayCount == 6)
            this.GetComponent<SpriteRenderer>().sprite = Day2;
        else if (CostManager.dayCount == 7)
            this.GetComponent<SpriteRenderer>().sprite = Day1;
        else if (CostManager.dayCount == 8)
            this.GetComponent<SpriteRenderer>().sprite = DDay;
        else
            this.GetComponent<SpriteRenderer>().sprite = DDay;
    }
}
