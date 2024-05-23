using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{   
    public static Setting Inst { get; private set; }
    void Awake() => Inst = this;

    public GameObject SettingPanel;
    
    private void OnMouseDown()
    {
        SettingPanel.SetActive(true);
        Menu.Inst.HideMenu();
    }
}
