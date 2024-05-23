using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyButtonHighlight : MonoBehaviour
{
    [SerializeField] GameObject StartButton;
    [SerializeField] GameObject EndButton;
    [SerializeField] Sprite StartButton_normal;
    [SerializeField] Sprite StartButton_highlight;
    [SerializeField] Sprite EndButton_normal;
    [SerializeField] Sprite EndButton_highlight;

    void Start() {
        StartButton.GetComponent<SpriteRenderer>().sprite = StartButton_normal;
        EndButton.GetComponent<SpriteRenderer>().sprite = EndButton_normal;
    }
    void OnMouseEnter() {
        if (gameObject.name == "Start Button") {
            StartButton.GetComponent<SpriteRenderer>().sprite = StartButton_highlight;
        }
        if (gameObject.name == "End Button") {
            EndButton.GetComponent<SpriteRenderer>().sprite = EndButton_highlight;
        }
    }
    void OnMouseExit() {
        if (gameObject.name == "Start Button") {
            StartButton.GetComponent<SpriteRenderer>().sprite = StartButton_normal;
        }
        if (gameObject.name == "End Button") {
            EndButton.GetComponent<SpriteRenderer>().sprite = EndButton_normal;
        }
    }
}
