using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EnvironmentalSubtitleManagmentSystem : MonoBehaviour
{
    KeyboardDatabaseDTO keyProfile;
    [SerializeField] private GameObject subtitleParentGameObject;
    [SerializeField] TextMeshProUGUI subtitleText;
    [SerializeField] float textAnimationDurationPerLetter;
    [SerializeField] private GameObject skipIndicator;
    private Coroutine previousSubtitlesCoroutine;
    Movement playerMovement;

    [SerializeField] private AudioSource uiSFX;

    bool isPaused = false;
    
    public EnvironmentalSubtitlesDTO environmentalSubtitlesDto;
    int currentIndex = 0;

    private void Start()
    {
        playerMovement = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (environmentalSubtitlesDto != null)
        {
            playerMovement.enabled = false;
            subtitleParentGameObject.SetActive(true);

            if (environmentalSubtitlesDto.subtitles.Count == 0)
            {
                environmentalSubtitlesDto.gameObject.SetActive(false);
                Reset();
            }
            
            if (environmentalSubtitlesDto.subtitles.Count > currentIndex)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    uiSFX.Play();
                    isPaused = false;
                    skipIndicator.SetActive(false);
                    currentIndex++;
                }
            }
            else
            {
                environmentalSubtitlesDto.gameObject.SetActive(false);
                Reset();
            }
            
            if (isPaused == false && environmentalSubtitlesDto != null)
            {
                ShowSubtitles(environmentalSubtitlesDto.subtitles[currentIndex]);
            }
            
        }
    }

    private void Reset()
    {
        playerMovement.enabled = true;
        currentIndex = 0;
        environmentalSubtitlesDto = null;
        subtitleParentGameObject.SetActive(false);
    }

    public void ShowSubtitles(string text)
    {
        isPaused = true;
        if (previousSubtitlesCoroutine != null)
        {
            StopCoroutine(previousSubtitlesCoroutine);
        }
        
        subtitleText.text = "";
        previousSubtitlesCoroutine = StartCoroutine(WriteAnimation(text));
    }
    
    private IEnumerator WriteAnimation(string targetText)
    {
        for (int i = 0; i < targetText.Length; i++)
        {
            yield return new WaitForSeconds(textAnimationDurationPerLetter);
            subtitleText.text = targetText.Substring(0, i + 1);
        }
        skipIndicator.SetActive(true);
    }
}
