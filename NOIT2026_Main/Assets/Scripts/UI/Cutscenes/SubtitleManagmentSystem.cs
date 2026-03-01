using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SubtitleManagmentSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText;
    [SerializeField] float textAnimationDurationPerLetter;
    [SerializeField] private bool isACutscene;
    bool isPaused = false;
    [SerializeField] private GameObject skipIndicator;
    Animator animator;

    [Header("Earthquake effect")] [SerializeField]
    float earthquakeMultiplier;

    [SerializeField] private float effectDurationSpeed;
    private bool earthquakeEffectOn = false;
    float originalPosX;
    float originalPosY;
    
    private Coroutine previousSubtitlesCoroutine;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (subtitleText != null)
        {
            originalPosX = subtitleText.transform.position.x;
            originalPosY = subtitleText.transform.position.y;
        }
    }

    private void Update()
    {
        if (isPaused == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            //subtitleText.text = "";
            skipIndicator.SetActive(false);
            isPaused = false;
            animator.speed = 1;
        }
    }

    public void ShowSubtitles(string text)
    {
        if (isACutscene)
        {
            animator.speed = 0;
            isPaused = true;
        }
        if (previousSubtitlesCoroutine != null)
        {
            StopCoroutine(previousSubtitlesCoroutine);
        }
        
        subtitleText.text = "";
        previousSubtitlesCoroutine = StartCoroutine(WriteAnimation(text));
    }

    public void ToggleEarthquakeEffect()
    {
        earthquakeEffectOn = !earthquakeEffectOn;
        StartCoroutine(Earthquake());
    }

    private IEnumerator WriteAnimation(string targetText)
    {
        for (int i = 0; i < targetText.Length; i++)
        {
            yield return new WaitForSeconds(textAnimationDurationPerLetter);
            subtitleText.text =  targetText.Substring(0, i + 1);
        }
        if(isACutscene)
        {
            skipIndicator.SetActive(true);
        }
    }


    public IEnumerator Earthquake()
    {
        while (earthquakeEffectOn)
        {
            var randomPosX = originalPosX + Random.Range(-1 * earthquakeMultiplier, 1 * earthquakeMultiplier);
            var randomPosY = originalPosY + Random.Range(-1 * earthquakeMultiplier, 1 * earthquakeMultiplier);
            subtitleText.transform.position = new Vector3(randomPosX, randomPosY);
            yield return new WaitForSeconds(effectDurationSpeed);
        }

        subtitleText.transform.position = new Vector3(originalPosX, originalPosY);
    }
}