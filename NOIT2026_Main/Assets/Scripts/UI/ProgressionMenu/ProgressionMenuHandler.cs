using System;
using TMPro;
using UnityEngine;

public class ProgressionMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject progressDescription;
    [SerializeField] private GameObject fragment1_GO_unfinished;
    [SerializeField] private GameObject fragment2_GO_unfinished;
    
    [SerializeField] private GameObject fragment1_GO_finished;
    [SerializeField] private GameObject fragment2_GO_finished;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Fragment 1") == 1);
        {
            fragment1_GO_unfinished.SetActive(false);
            fragment1_GO_finished.SetActive(true);
        }
        
        if (PlayerPrefs.GetInt("Fragment 2") == 1);
        {
            fragment2_GO_unfinished.SetActive(false);
            fragment2_GO_finished.SetActive(true);
        }
    }

    public void ShowProgressDescription()
    {
        progressDescription.SetActive(true);
    }

    public void HideProgressDescription()
    {
        progressDescription.SetActive(false);
    }
}