using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] int playButtonIndex;
    [SerializeField] private Image transitionPanel;
    [SerializeField] private float duration;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject progressMenuPanel;
    [SerializeField] AudioSource UIAudioSource;
    bool isFading = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    public void Play()
    {
        if (!isFading)
        {
            StartCoroutine(PlayFadeIn());
            isFading = true;
        }
    }   

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        mainMenuPanel.SetActive(!mainMenuPanel.activeSelf);
    }

    public void ToggleProgressMenu()
    {
        mainCamera.SetActive(!mainCamera.activeSelf);
        progressMenuPanel.SetActive(!progressMenuPanel.activeSelf);
        mainMenuPanel.SetActive(!mainMenuPanel.activeSelf);
    }
    
    public void PlaySound()
    {
        UIAudioSource.PlayOneShot(UIAudioSource.clip);
    }

    public IEnumerator PlayFadeIn()
    {
        transitionPanel.gameObject.SetActive(true);

        for (float i = 0; i < 1; i+=0.01f)
        {
            transitionPanel.color = new Color(transitionPanel.color.r, transitionPanel.color.g, transitionPanel.color.b, i);
            yield return new WaitForSecondsRealtime(duration);
        }
        SceneManager.LoadScene(playButtonIndex);
    }
}
