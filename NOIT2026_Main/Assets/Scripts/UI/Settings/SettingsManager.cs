using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject controlsSettingsPanel;
    [SerializeField] GameObject generalSettingsPanel;
    
    [SerializeField] GameObject generalSettingsToggleIcon;
    [SerializeField] GameObject controlsSettingsToggleIcon;
    

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
