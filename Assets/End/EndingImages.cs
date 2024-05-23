using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingImages : MonoBehaviour
{
    [SerializeField] GameObject E_background;
    [SerializeField] GameObject D_character;
    [SerializeField] GameObject D_effect;
    [SerializeField] GameObject D_image;
    void Update()
    {
        Color B_color = E_background.GetComponent<SpriteRenderer>().color;
        Color D_color = D_character.GetComponent<SpriteRenderer>().color;
        Color E_color = D_effect.GetComponent<SpriteRenderer>().color;
        Color I_color = D_image.GetComponent<SpriteRenderer>().color;
        B_color.a = this.GetComponent<SpriteRenderer>().color.a;
        D_color.a = this.GetComponent<SpriteRenderer>().color.a;
        E_color.a = this.GetComponent<SpriteRenderer>().color.a;
        I_color.a = this.GetComponent<SpriteRenderer>().color.a;
    }
}
