using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinalizationHandling : MonoBehaviour
{
    [SerializeField] private List<GameObject> fragments;

    [SerializeField] private int transitionSceneIndex;

    void Update()
    {
        if (fragments.TrueForAll(x => x.activeSelf == false))
        {
            SceneManager.LoadScene(transitionSceneIndex);
        }
    }
}
