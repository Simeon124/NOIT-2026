using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class KeybindManager : MonoBehaviour
{
    KeyboardDatabaseDTO overrideKeyboardConfiguration;
    bool isListening = false;
    Action currentListeningAction;
    
    [SerializeField] KeyboardDatabase defaultKeyboardDatabase;
    KeyboardDatabaseDTO currentKeyboardDatabase;

    [SerializeField] private GameObject listeningPanel;
    
    void Awake()
    {
        overrideKeyboardConfiguration = new KeyboardDatabaseDTO();
        string save = PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName);
        if (!string.IsNullOrEmpty(save))
        {
            currentKeyboardDatabase = JsonUtility.FromJson<KeyboardDatabaseDTO>(save);
        }
        else
        {
            KeyboardDatabaseDTO dto = new KeyboardDatabaseDTO();
            dto.Actions = defaultKeyboardDatabase.Actions.ToList();
            PlayerPrefs.SetString(GlobalConfig.keybindSavePropertyName, JsonUtility.ToJson(dto));   
            currentKeyboardDatabase.Actions = defaultKeyboardDatabase.Actions.ToList();
        }
        
    }
    
    void Update()
    {
        if (isListening)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(vKey))
                {
                    isListening = false;
                    overrideKeyboardConfiguration.Actions.First(x => x.Key == currentListeningAction).Value = vKey;
                    PlayerPrefs.SetString(GlobalConfig.keybindSavePropertyName, JsonUtility.ToJson(currentKeyboardDatabase));
                    break;
                }
            }

            foreach (var component in GameObject.FindObjectsByType<KeybindUI>(FindObjectsSortMode.None))
            {
                component.Initialize();
            }
            listeningPanel.SetActive(isListening);
        }
    }

    public void ChangeActionKey(KeybindUI keybindUI)
    {
        overrideKeyboardConfiguration.Actions = currentKeyboardDatabase.Actions.ToList();
        isListening = true;
        currentListeningAction = keybindUI.Action;
    }
}