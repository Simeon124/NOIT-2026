using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinalizationHandling : MonoBehaviour
{
    [SerializeField] private List<GameObject> fragments;

    [SerializeField] private int transitionSceneIndex;
    
    [SerializeField] private GameStateSaveSystem gameStateSaveSystem;

    void Update()
    {
        if (fragments.TrueForAll(x => x.activeSelf == false))
        {
            gameStateSaveSystem.ClearFragments();
            SceneManager.LoadScene(transitionSceneIndex);
        }
    }
}
