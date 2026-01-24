using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SubtitleManagmentSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText;
    [SerializeField] float textAnimationDurationPerLetter;
    [Header("Earthquake effect")]
    [SerializeField] float earthquakeMultiplier;
    [SerializeField] private float effectDurationSpeed;
    private bool earthquakeEffectOn = false;
    float originalPosX;
    float originalPosY;

    private void Start()
    {
        originalPosX = subtitleText.transform.position.x;
        originalPosY = subtitleText.transform.position.y;
    }

    public void ShowSubtitles(string text)
    {
        subtitleText.text = "";
        StartCoroutine(WriteAnimation(text));
    }

    public void ToggleEarthquakeEffect()
    {
        earthquakeEffectOn = !earthquakeEffectOn;
        StartCoroutine(Earthquake());
    }

    private IEnumerator WriteAnimation(string targetText)
    {
        foreach (var letter in targetText)
        {
            if (letter != '\\')
            {
                yield return new WaitForSeconds(textAnimationDurationPerLetter);
                subtitleText.text += letter;
            }
            else
            {
                subtitleText.text += letter;
            }
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
        
        Camera.main.transform.position = new Vector3(originalPosX, originalPosY);
    }
}