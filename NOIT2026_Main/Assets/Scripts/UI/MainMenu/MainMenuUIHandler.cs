using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] int PlayButtonIndex;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject mainMenuPanel;
    
    public void Play()
    {
        SceneManager.LoadScene(PlayButtonIndex);
    }

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        mainMenuPanel.SetActive(!mainMenuPanel.activeSelf);
    }
}
