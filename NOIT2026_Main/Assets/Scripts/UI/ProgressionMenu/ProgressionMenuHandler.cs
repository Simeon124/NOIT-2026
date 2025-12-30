using UnityEngine;

public class ProgressionMenuHandler : MonoBehaviour
{
    [SerializeField] int currentLevel = 0;
    [SerializeField] GameObject[] storySegmentsGameObjects;
    void Start()
    {
        for (int i = 0; i <= currentLevel; i++)
        {
            if (i < storySegmentsGameObjects.Length)
            {
                storySegmentsGameObjects[i].SetActive(true);
            }
        }
    }
}
