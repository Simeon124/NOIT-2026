using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PianoHandler : MonoBehaviour
{
    KeyboardDatabaseDTO keyProfile;
    AudioSource audioSource;
    bool inRange;
    private int counter = -1;

    [SerializeField] private AudioClip[] notes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyProfile = JsonUtility.FromJson<KeyboardDatabaseDTO>(PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName));
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(keyProfile.Actions.First(x => x.Key == Action.Interact).Value))
        {
            counter++;
            if (counter >= notes.Length)
            {
                counter = -1;
            }
            else
            {
                audioSource.PlayOneShot(notes[counter]);
            }
        }
    }

    void OnTriggerEnter(Collider other)
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
