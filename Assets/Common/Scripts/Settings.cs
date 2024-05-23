using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{   
    public static Setting Inst { get; private set; }
    void Awake() => Inst = this;

    public GameObject SettingPanel;
    [SerializeField] Slider volumeSlider;
    
    private void Start()
    {
        volumeSlider.value = 0.5f;
        volumeSlider.onValueChanged.AddListener((value) => Audio.Inst.audioSource.volume = value);
    }

    private void OnMouseDown()
    {
        SettingPanel.SetActive(true);
        Menu.Inst.HideMenu();
    }
}
