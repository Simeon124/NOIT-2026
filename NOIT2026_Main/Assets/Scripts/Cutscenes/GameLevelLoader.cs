using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelLoader : MonoBehaviour
{
    //This script handles that the player arrives at the correct save after the intro scene.

    //CAUTION: The first element is always the interlude main menu. The second - first level main menu etc.
    [SerializeField] private List<int> startMenusBuildIndexes;
    private int currentLevel;

    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }

    void Load()
    {
        SceneManager.LoadScene(startMenusBuildIndexes[currentLevel]);
    }
}