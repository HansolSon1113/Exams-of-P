using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour{
    private void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 objPosition = new Vector2(mousePosition.x, mousePosition.y);
        transform.position = mousePosition;
    }
}
