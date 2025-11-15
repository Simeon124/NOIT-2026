using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToAScene : MonoBehaviour
{
    bool inRange;
    [SerializeField] bool isPuzzle; 
    [SerializeField] int sceneIndex;
    Movement movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GameObject.FindAnyObjectByType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(movement.interactKey) && inRange == true)
        {
            if(isPuzzle == true)
            {
                var saveSystem = FindAnyObjectByType<GameStateSaveSystem>();
                saveSystem.SaveGame();
            }
            SceneManager.LoadScene(sceneIndex);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
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
