using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject controlsSettingsPanel;
    [SerializeField] GameObject generalSettingsPanel;
    [SerializeField] private TMP_Dropdown graphicsSettingsDropdown;
    
    [SerializeField] GameObject generalSettingsToggleIcon;
    [SerializeField] GameObject controlsSettingsToggleIcon;

    private void Start()
    {
        var audioSlider = GameObject.Find("AudioSlider").GetComponent<Slider>();
        audioMixer.GetFloat("Volume", out var value);
        if (value != 0)
        {
            audioSlider.value = value;
        }
        
        var mouseSenSlider = GameObject.Find("SensitivitySlider").GetComponentInChildren<Slider>();
        var mouseSenProperty = PlayerPrefs.GetFloat(GlobalConfig.mouseSensitivitySavePropertyName);
        if (mouseSenProperty != 0)
        {
            mouseSenSlider.value = mouseSenProperty;
        }
        else
        {
            PlayerPrefs.SetFloat(GlobalConfig.mouseSensitivitySavePropertyName, mouseSenSlider.value);
        }
        
        graphicsSettingsDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void ChangeGraphicalSettings()
    {
        switch (graphicsSettingsDropdown.value)
        {
            case 0:
                QualitySettings.SetQualityLevel(0);
                break;
            case 1:
                QualitySettings.SetQualityLevel(1);
                break;
            case 2:
                QualitySettings.SetQualityLevel(2);
                break;
        }
    }

    public void ChangeVolume(Slider slider)
    {
        if (slider.value == slider.minValue)
        {
            audioMixer.SetFloat("Volume", -80);
        }
        else
        {
            audioMixer.SetFloat("Volume", slider.value);
        }
    }

    public void ChangeMouseSensitivity(Slider slider)
    {
        PlayerPrefs.SetFloat(GlobalConfig.mouseSensitivitySavePropertyName, slider.value);
    }
    
    public void ToggleGeneralSettings()
    {
        generalSettingsToggleIcon.SetActive(true);
        controlsSettingsToggleIcon.SetActive(false);
        
        controlsSettingsPanel.SetActive(false);
        generalSettingsPanel.SetActive(true);
    }

    public void ToggleControlsSettings()
    {
        controlsSettingsToggleIcon.SetActive(true);
        generalSettingsToggleIcon.SetActive(false);
        
        controlsSettingsPanel.SetActive(true);
        generalSettingsPanel.SetActive(false);
    }
}
