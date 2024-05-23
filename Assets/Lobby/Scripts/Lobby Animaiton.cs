using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LobbyAnimaiton : MonoBehaviour
{
    [SerializeField] GameObject LobbyImages;
    Vector3 Locate = new Vector3(0, -0.33f, 0);
    void Start() {
        LobbyImages.transform.position = new Vector3(0, 10, 0);
        LobbyImages.transform.DOMove(new Vector3(0, -1, 0), 0.5f);
    }

}
