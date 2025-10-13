using System.Collections;
using TMPro;
using UnityEngine;

public class SubtitleManagmentSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText;
    [SerializeField] float textAnimationDurationPerLetter;

    public void ShowSubtitles(string text)
    {
        subtitleText.text = "";
        StartCoroutine(WriteAnimation(text));
    }

    public IEnumerator WriteAnimation(string targetText)
    {
        foreach (var letter in targetText)
        {
            yield return new WaitForSeconds(textAnimationDurationPerLetter);
            subtitleText.text += letter;
        }
    }
}
