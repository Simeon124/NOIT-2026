using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] AudioSource UIAudioSource;
    GlobalIngameTimeHandler globalIngameTimeHandler;
    [SerializeField] int InterludeIndex;
    [SerializeField] int Level1Index;
    [SerializeField] int Level2Index;
    
    Movement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<Movement>();
        globalIngameTimeHandler = FindObjectOfType<GlobalIngameTimeHandler>();
    }

    public void Resume()
    {
        this.gameObject.SetActive(false);
        globalIngameTimeHandler.gameIsPaused = false;

        if (playerMovement != null)
        {
            if (playerMovement.isActiveAndEnabled)
            {
                Cursor.lockState = CursorLockMode.Locked;      
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
    }   

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        mainMenuPanel.SetActive(!mainMenuPanel.activeSelf);
    }
    
    public void PlaySound()
    {
        UIAudioSource.PlayOneShot(UIAudioSource.clip);
    }

    public void GoToMainMenu()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        switch (currentLevel)
        {
            case 0:
                SceneManager.LoadScene(InterludeIndex);
                break;
            case 1:
                SceneManager.LoadScene(Level1Index);
                break;
            case 2:
                SceneManager.LoadScene(Level2Index);
                break;
        }
    }
}
