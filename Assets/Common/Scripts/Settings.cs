using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{   
    public static Setting Inst { get; private set; }
    void Awake() => Inst = this;

    public GameObject SettingPanel;
    [SerializeField] Slider backgroundVolumeSlider;
    [SerializeField] Slider popVolumeSlider;
    
    private void Start()
    {
        Time.timeScale = 1f;
        backgroundVolumeSlider.value = Audio.Inst.backgroundVolume;
        backgroundVolumeSlider.onValueChanged.AddListener((value) => Audio.Inst.backgroundVolume = value);
        popVolumeSlider.value = Audio.Inst.popVolume;
        popVolumeSlider.onValueChanged.AddListener((value) => Audio.Inst.popVolume = value);
    }

    private void OnMouseDown()
    {
        SettingPanel.SetActive(true);
        Menu.Inst.HideMenu();
    }
}
