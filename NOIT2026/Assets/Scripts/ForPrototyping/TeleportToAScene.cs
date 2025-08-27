using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToAScene : MonoBehaviour
{
    bool inRange;
    [SerializeField] bool notPuzzle; 
    [SerializeField] int sceneIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inRange == true)
        {
            if(notPuzzle == true)
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
}
