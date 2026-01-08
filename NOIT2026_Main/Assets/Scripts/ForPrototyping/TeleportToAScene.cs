using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToAScene : MonoBehaviour
{
    bool inRange;
    [SerializeField] bool isPuzzle; 
    [SerializeField] int sceneIndex;
    Movement movement;
    KeyboardDatabaseDTO keyProfile;
    [Header("Animation")]
    Animator animator;
    [SerializeField] float animationDuration;

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
        if(interactableBtnPressed && inRange == true)
        {
            if(isPuzzle == true)
            {
                var saveSystem = FindAnyObjectByType<GameStateSaveSystem>();
                saveSystem.SaveGame();
            }

            if (animator != null)
            {
                animator.SetTrigger("interacted");
                StartCoroutine(WaitForAnimation());
            }
            else
            {
                SceneManager.LoadScene(sceneIndex);
            }
            
        }
    }

    public IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(animationDuration);
        SceneManager.LoadScene(sceneIndex);
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
