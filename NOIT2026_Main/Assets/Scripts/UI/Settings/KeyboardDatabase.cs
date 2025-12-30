using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Keybinds/KeyboardDatabase")]
public class KeyboardDatabase : ScriptableObject
{
    public List<KeyDictionary> Actions;
}

public class KeyboardDatabaseDTO
{
    public List<KeyDictionary> Actions;
}

[System.Serializable]
public class KeyDictionary
{
    public Action Key;
    public KeyCode Value;
}

[System.Serializable]
public enum Action
{
    Interact,
    Jump,
    Flashlight,
    Sprint
}
