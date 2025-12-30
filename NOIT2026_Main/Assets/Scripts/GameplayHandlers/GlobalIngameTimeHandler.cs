using UnityEngine;

public class GlobalIngameTimeHandler : MonoBehaviour
{
    public bool gameIsPaused = false;
    
    void Update()
    {
        if(gameIsPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
