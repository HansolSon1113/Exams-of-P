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
        Sprite D_day_image = this.GetComponent<SpriteRenderer>().sprite;
        print(CostManager.dayCount);
        if (CostManager.dayCount == 1)
            D_day_image = Day7;
        else if (CostManager.dayCount == 2)
            D_day_image = Day6;
        else if (CostManager.dayCount == 3)
            D_day_image = Day5;
        else if (CostManager.dayCount == 4)
            D_day_image = Day4;
        else if (CostManager.dayCount == 5)
            D_day_image = Day3;
        else if (CostManager.dayCount == 6)
            D_day_image = Day2;
        else if (CostManager.dayCount == 7)
            D_day_image = Day1;
        else if (CostManager.dayCount == 8)
            D_day_image = DDay;
        else
            D_day_image = DDay;
    }
}
