using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTarget : MonoBehaviour
{
    private void onTriggerEnter(Collision other)
    {
        if(other.gameObject.tag == "Card")
        {
            Destroy(this.gameObject);
        }
    }
}
