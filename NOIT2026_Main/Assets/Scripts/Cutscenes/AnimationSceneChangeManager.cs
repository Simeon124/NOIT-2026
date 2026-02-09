using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationSceneChangeManager : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 0;
    public void ChangeToSceneIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    
    public void DynamicChangeToSceneIndex()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
