using System;
using UnityEngine;

public class BackgroundAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundWindAudio;
    [SerializeField] private float targetVolume;
    [SerializeField] private float targetPitch;
    Movement movementScript;
    float defaultWindAudioVolume;

    [SerializeField] private string houseTag;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementScript = GameObject.FindAnyObjectByType<Movement>();
        defaultWindAudioVolume = backgroundWindAudio.volume;
    }

    void Update()
    {
        if (movementScript.sanityScr.currentZones.Contains("InsanityZone") || movementScript.sanityScr.currentZones.Contains("SafeZone"))
        {
            backgroundWindAudio.pitch = targetPitch;
            backgroundWindAudio.volume = targetVolume;
        }
        else
        {
            backgroundWindAudio.pitch = 1f;
            backgroundWindAudio.volume = defaultWindAudioVolume;
        }
    }
}
