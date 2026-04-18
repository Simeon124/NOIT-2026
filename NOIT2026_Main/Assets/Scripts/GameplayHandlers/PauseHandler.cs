using System.Linq;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    KeyboardDatabaseDTO keyProfile;
    
    GlobalIngameTimeHandler globalIngameTimeHandler;
    
    Movement playerMovement;
    
    [SerializeField] private GameObject pauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = FindObjectOfType<Movement>();
        globalIngameTimeHandler = FindObjectOfType<GlobalIngameTimeHandler>();
        keyProfile =
            JsonUtility.FromJson<KeyboardDatabaseDTO>(PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyProfile.Actions.First(x => x.Key == Action.Pause).Value) && globalIngameTimeHandler.gameIsPaused == false)
        {
            pauseMenu.SetActive(true);
            globalIngameTimeHandler.gameIsPaused = true;
            if (playerMovement != null)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
