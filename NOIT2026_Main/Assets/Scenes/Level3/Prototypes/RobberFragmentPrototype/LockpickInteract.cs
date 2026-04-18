using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LockpickInteract : MonoBehaviour
{
    bool inRange;
    public GameObject LockImage;
    Movement movement;
    KeyboardDatabaseDTO keyProfile;
    [Header("Animation")]
    Animator animator;
    bool cursorLocked = true;
    //ADD CAMERA TO STOP FROM MOVING

    void Start()
    {
        keyProfile = JsonUtility.FromJson<KeyboardDatabaseDTO>(PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName));
        movement = GameObject.FindAnyObjectByType<Movement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var interactableBtnPressed = Input.GetKeyDown(keyProfile.Actions.First(x => x.Key == Action.Interact).Value);
        if (interactableBtnPressed && inRange == true)
        {
            LockImage.SetActive(!LockImage.activeSelf);
            movement.enabled = !movement.isActiveAndEnabled;
            if (cursorLocked)
            { 
                Cursor.lockState = CursorLockMode.None; cursorLocked = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked; cursorLocked = true;
            }
            // ADD FUNCTIONALITY!!!
            if (animator != null)
            {
            }
            else
            {
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }
}
