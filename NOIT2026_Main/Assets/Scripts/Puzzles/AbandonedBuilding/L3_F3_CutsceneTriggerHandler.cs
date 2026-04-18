using UnityEngine;
using UnityEngine.SceneManagement;

public class L3_F3_CutsceneTriggerHandler : MonoBehaviour
{
    [SerializeField] private GameObject trigger;
    [SerializeField] private int transitionCutsceneIndex;

    void Update()
    {
        if (!trigger.activeSelf)
        {
            SceneManager.LoadScene(transitionCutsceneIndex);
        }
    }
}
