using TMPro;
using UnityEngine;

public class ProgressionMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject progressDescription;

    public void ShowProgressDescription()
    {
       progressDescription.SetActive(true);
    }

    public void HideProgressDescription()
    {
        progressDescription.SetActive(false);
    }
}
