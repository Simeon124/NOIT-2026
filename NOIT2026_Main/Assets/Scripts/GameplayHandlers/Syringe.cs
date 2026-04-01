using System;
using System.Linq;
using UnityEngine;

public class Syringe : MonoBehaviour
{
    KeyboardDatabaseDTO keyProfile;
    private SyringeHandler syringeHandler;
    private bool inRange;
    void Start()
    {
        keyProfile = JsonUtility.FromJson<KeyboardDatabaseDTO>(PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName));
        syringeHandler = FindFirstObjectByType<SyringeHandler>();
    }

    void Update()
    {
        var interactionBtnPressed = Input.GetKeyDown(keyProfile.Actions.First(x => x.Key == Action.Interact).Value);
        if (inRange && interactionBtnPressed)
        {
            Destroy(this.gameObject);
            syringeHandler.ConsumeSyringe();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
