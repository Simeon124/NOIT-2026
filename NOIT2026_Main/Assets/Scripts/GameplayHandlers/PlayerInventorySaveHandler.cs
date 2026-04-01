using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventorySaveHandler : MonoBehaviour
{
    PlayerInventory playerInventory;
    void Start()
    {
        playerInventory = GameObject.FindFirstObjectByType<PlayerInventory>();
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetString("PlayerInventory", JsonUtility.ToJson(playerInventory.items));
    }

    public void Load()
    {
        var inventoryItemsSave = PlayerPrefs.GetString(GlobalConfig.playerInventorySavePropertyName);
        if (!String.IsNullOrEmpty(inventoryItemsSave))
        {
            var deSerializedInventoryItemsSave = JsonUtility.FromJson<List<Item>>(inventoryItemsSave);
            playerInventory.items = deSerializedInventoryItemsSave.ToList();
        }
    }
}
