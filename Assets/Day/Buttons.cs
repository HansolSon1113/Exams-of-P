using UnityEngine;

public class Buttons : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (gameObject.name == "Night Button Circle")
        {
            SceneChangeAnimation circleAnimation = GetComponent<SceneChangeAnimation>();
            circleAnimation.Day2Night_Circle();
        }
        else if (gameObject.name == "Night Button Bright")
        {
            SceneChangeAnimation BrightAnimation = GetComponent<SceneChangeAnimation>();
            BrightAnimation.Day2Night_Bright();
        }
        else if (gameObject.name == "Draw Button")
        {
            CardManager.Inst.draw();
        }
    }
}
