using TMPro;
using UnityEngine;

public class ProgressionMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject progressDescription;

    public void ShowProgressDescription(string description)
    {
       var text =  progressDescription.GetComponentInChildren<TextMeshProUGUI>();
       text.text = description;
       progressDescription.SetActive(true);
    }

    public void HideProgressDescription()
    {
        progressDescription.SetActive(false);
    }
}
