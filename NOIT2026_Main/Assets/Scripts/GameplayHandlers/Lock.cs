using System;
using System.Linq;
using UnityEngine;

public class Lock : MonoBehaviour
{
    KeyboardDatabaseDTO keyProfile;
    [SerializeField] GameObject lockedState;
    [SerializeField] GameObject unlockedState;
    public bool isUnlocked;
    private bool inRange;
    UnlockedCheck check;
    private ActivateLocker[] activateLockerScripts;
    [SerializeField] GameObject player;
    [SerializeField] GameObject lockpickSystemGO;
    [SerializeField] AudioSource unlockSFX;
    

    private void Start()
    {
        keyProfile = JsonUtility.FromJson<KeyboardDatabaseDTO>(PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName));
    }

    void Update()
    {
        if (check != null)
        {Debug.Log(check.unlocked);}
        
        var interactBtnPressed = Input.GetKeyDown(keyProfile.Actions.First(x => x.Key == Action.Interact).Value);

        if (interactBtnPressed && inRange)
        {
            ToggleLockpickSystem();
        }
        if (check != null)
        {Debug.Log(check.unlocked);}
        if (isUnlocked)
        {
            unlockedState.SetActive(true);
            lockedState.SetActive(false);
            check.unlocked = false;
            foreach (var pin in check.pins)
            {
                pin.locker = false;
            }

            foreach (var script in activateLockerScripts)
            {
                script.Bcol.enabled = false;
                script.text.enabled = true;
            }
            
            check.ResetPositions();
            check = null;
            player.SetActive(true);
            unlockSFX.Play();
            lockpickSystemGO.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            
            isUnlocked = !isUnlocked;
            Destroy(gameObject);
        }
        if (check != null)
        {Debug.Log(check.unlocked);}
        if (check != null && !isUnlocked)
        {
            if (check.unlocked == true)
            {
                isUnlocked = true;
            }
        }
        if (check != null)  
        {Debug.Log(check.unlocked);}
    }

    private void ToggleLockpickSystem()
    {
        inRange = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        player.SetActive(false);
        lockpickSystemGO.SetActive(true);
        check = FindAnyObjectByType<UnlockedCheck>();
        activateLockerScripts =  FindObjectsByType<ActivateLocker>(sortMode:FindObjectsSortMode.None);
        check.unlocked = false;
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
