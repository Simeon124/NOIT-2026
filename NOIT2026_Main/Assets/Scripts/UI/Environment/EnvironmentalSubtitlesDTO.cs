using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnvironmentalSubtitlesDTO : MonoBehaviour
{
    public List<string> subtitles;
    
    KeyboardDatabaseDTO keyProfile;
    EnvironmentalSubtitleManagmentSystem environmentalSubtitleManagement;
    private bool inRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyProfile = JsonUtility.FromJson<KeyboardDatabaseDTO>(PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName));
        environmentalSubtitleManagement =  FindObjectOfType<EnvironmentalSubtitleManagmentSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(keyProfile.Actions.First(x => x.Key == Action.Interact).Value))
        {
            environmentalSubtitleManagement.environmentalSubtitlesDto = this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
