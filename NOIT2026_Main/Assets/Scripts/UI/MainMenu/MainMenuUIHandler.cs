using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] int PlayButtonIndex;
    
    public void Play()
    {
        SceneManager.LoadScene(PlayButtonIndex);
    }
}
