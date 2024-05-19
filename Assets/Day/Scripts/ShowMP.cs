using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMP : MonoBehaviour
{
    public static ShowMP Inst { get; private set; }
    private void Awake() => Inst = this;

    [SerializeField] GameObject MPChatField;
    [SerializeField] Transform MPChatSpawnPosition;
    public bool isShowing = false;
    private void OnMouseDown()
    {
        if(isShowing == false)
        {
            isShowing = true;
            Instantiate(MPChatField, MPChatSpawnPosition.position, MPChatSpawnPosition.rotation);
        }
    }
}
