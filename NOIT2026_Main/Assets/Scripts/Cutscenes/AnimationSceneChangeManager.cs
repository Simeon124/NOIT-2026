using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationSceneChangeManager : MonoBehaviour
{
    public void ChangeToSceneIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
