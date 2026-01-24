using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinalizationHandling : MonoBehaviour
{
    [SerializeField] private List<GameObject> fragments;

    [SerializeField] private GameObject transitionAnimationGO;

    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject backgSFX;
    
    [SerializeField] private GameStateSaveSystem gameStateSaveSystem;

    void Update()
    {
        if (fragments.TrueForAll(x => x.activeSelf == false))
        {
            gameStateSaveSystem.ClearFragments();
            transitionAnimationGO.SetActive(true);
            playerGameObject.SetActive(false);
            backgSFX.SetActive(false);
        }
    }
}
