using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditExit : MonoBehaviour
{
    [SerializeField] Sprite Default_icon;
    [SerializeField] Sprite Clicked_icon;
    [SerializeField] GameObject Main_Camera;
    
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Default_icon;
        AudioSource CreditMusic = Main_Camera.GetComponent<AudioSource>();
        CreditMusic.Play();
    }

    void OnMouseDown() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Clicked_icon;
        AudioSource CreditMusic = Main_Camera.GetComponent<AudioSource>();
        CreditMusic.Stop();
        SceneManager.LoadScene("Lobby");
    }
}
