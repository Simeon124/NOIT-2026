using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject controlsSettingsPanel;
    [SerializeField] GameObject generalSettingsPanel;
    

    void ChangeVolume(Slider slider)
    {
        audioMixer.SetFloat("Volume", slider.value);
    }
    
    public void ToggleGeneralSettings()
    {
        controlsSettingsPanel.SetActive(false);
        generalSettingsPanel.SetActive(true);
    }

    public void ToggleControlsSettings()
    {
        controlsSettingsPanel.SetActive(true);
        generalSettingsPanel.SetActive(false);
    }
}
