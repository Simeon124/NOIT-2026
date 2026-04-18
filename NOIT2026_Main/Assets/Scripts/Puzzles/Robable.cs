using System;
using System.Linq;
using UnityEngine;

public class Robable : MonoBehaviour
{
    private bool inRange;
    KeyboardDatabaseDTO keyProfile;
    [SerializeField] AudioSource pickupSound;
    void Start()
    {
        keyProfile = JsonUtility.FromJson<KeyboardDatabaseDTO>(PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName));
    }

    void Update()
    {
        var interactionBtnPressed = Input.GetKeyDown(keyProfile.Actions.First(x => x.Key == Action.Interact).Value);
        if (interactionBtnPressed && inRange)
        {
            if (pickupSound != null)
            {
                pickupSound.Play();
            }
            Destroy(gameObject);
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
