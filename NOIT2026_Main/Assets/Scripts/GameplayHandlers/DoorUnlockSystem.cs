using System;
using System.Linq;
using UnityEngine;

public class DoorUnlockSystem : MonoBehaviour
{
    [SerializeField] private LayerMask defaultLayer;
    private Rigidbody rb;
    PlayerInventory playerInventory;
    bool isUnlocked;
    KeyboardDatabaseDTO keyProfile;
    private bool inRange;
    [SerializeField] AudioSource unlockSound;
    [SerializeField] AudioSource lockedSound;
    [SerializeField] GameObject interactionIndicator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyProfile =
            JsonUtility.FromJson<KeyboardDatabaseDTO>(PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName));
        rb = GetComponent<Rigidbody>();
        playerInventory = GameObject.FindAnyObjectByType<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUnlocked)
        {
            var interactKey = keyProfile.Actions.First(x => x.Key == Action.Interact).Value;
            if (playerInventory.items != null)
            {
                if (inRange && Input.GetKeyDown(interactKey) && playerInventory.items
                        .Where(x => x.GetType() == typeof(Key))
                        .Select(x => x as Key)
                        .FirstOrDefault(x => x.door == this.gameObject))
                {
                    Unlock();
                }
                else if(inRange && Input.GetKeyDown(interactKey))
                {
                    if (lockedSound != null)
                    {
                        lockedSound.Play();
                    }
                }
            }
        }
    }

    void Unlock()
    {
        if (interactionIndicator != null)
        {
            interactionIndicator.SetActive(false);
        }
        gameObject.layer = defaultLayer;
        isUnlocked = true;
        rb.isKinematic = false;

        if (unlockSound != null)
        {
            unlockSound.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
