using System.Linq;
using TMPro;
using UnityEngine;

public class InteractionTextHandler : MonoBehaviour
{
    KeyboardDatabaseDTO keyProfile;
    TextMeshProUGUI text;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        
        keyProfile =
            JsonUtility.FromJson<KeyboardDatabaseDTO>(PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName));
    }

    // Update is called once per frame
    void Update()
    {
        text.text = keyProfile.Actions.First(x => x.Key == Action.Interact).Value.ToString();
    }
}
