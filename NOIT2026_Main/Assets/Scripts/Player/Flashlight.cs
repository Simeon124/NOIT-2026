using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    KeyboardDatabaseDTO keyProfile;
    [SerializeField] GameObject flashlight;
    [SerializeField] bool isOn = false;
    [SerializeField] Image flashlightImage;
    private Movement movement;

    [SerializeField] private AudioSource turnOnSFX;
    [SerializeField] private AudioSource turnOffSFX;

    private void Start()
    {
        keyProfile = JsonUtility.FromJson<KeyboardDatabaseDTO>(PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName));
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        var flashlightBtnPressed = Input.GetKeyDown(keyProfile.Actions.First(x => x.Key == Action.Flashlight).Value);
        if (flashlightBtnPressed)
        {
            isOn = !isOn;

            if (isOn == false)
            {
                turnOffSFX.Play();
            }
            else
            {
                turnOnSFX.Play();
            }
        }

        FlashlightControl();

    }

    void FlashlightControl()
    {
        if (isOn)
        {
            flashlightImage.color = new Color(flashlightImage.color.r, flashlightImage.color.g, flashlightImage.color.b, 1f);
            flashlight.SetActive(true);
        }
        else
        {
            flashlightImage.color = new Color(flashlightImage.color.r, flashlightImage.color.g, flashlightImage.color.b, 0.15f);
            flashlight.SetActive(false);
        }
    }
}
