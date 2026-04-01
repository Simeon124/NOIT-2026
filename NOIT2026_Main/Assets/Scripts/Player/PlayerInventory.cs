using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items;
    
    
    bool inRange = false;
    Item rangedItem;
    KeyboardDatabaseDTO keyProfile;
    PlayerInventorySaveHandler playerInventorySaveHandler;

    private void Start()
    {
        playerInventorySaveHandler = FindObjectOfType<PlayerInventorySaveHandler>();
        
        keyProfile =
            JsonUtility.FromJson<KeyboardDatabaseDTO>(PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName));
    }

    private void Update()
    {
        var interactKey = keyProfile.Actions.First(x => x.Key == Action.Interact).Value;
        if (inRange && Input.GetKeyDown(interactKey))
        {
            items.Add(rangedItem);
            playerInventorySaveHandler.Save();
            rangedItem.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Item>() != null)
        {
            rangedItem = other.gameObject.GetComponent<Item>();
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        rangedItem = null;
        inRange = false;
    }
}