using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeybindUI : MonoBehaviour
{
    [SerializeField] public Action Action;
    private KeybindManager keybindManager;
    [SerializeField] TextMeshProUGUI keyUI;
    [SerializeField] TextMeshProUGUI label;

    private void Start()
    {
        keybindManager = GameObject.FindAnyObjectByType<KeybindManager>();
        Initialize();
    }

    public void Initialize()
    {
        KeyboardDatabaseDTO keyProfile = JsonUtility.FromJson<KeyboardDatabaseDTO>(PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName));

        var currentAction = keyProfile.Actions.First(x => x.Key == Action);
        keyUI.text = currentAction.Value.ToString();
        label.text = currentAction.Key.ToString();
    }
}