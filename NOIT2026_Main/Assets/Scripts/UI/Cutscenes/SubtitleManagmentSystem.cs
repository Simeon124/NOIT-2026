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

    private IEnumerator WriteAnimation(string targetText)
    {
        Debug.Log("writing");
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
}