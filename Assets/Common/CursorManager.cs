using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D normalHand;
    [SerializeField] Texture2D highlightHand;
    [SerializeField] Texture2D clickedHand;

    private Texture2D defaultCursor;

    void Start()
    {
        defaultCursor = normalHand;
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseEnter()
    {
        Cursor.SetCursor(highlightHand, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseDown()
    {
        Cursor.SetCursor(clickedHand, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseUp()
    {
        Cursor.SetCursor(highlightHand, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
    }
}
