using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReloadCurrentScene(int time)
    {
        StartCoroutine(ReloadPuzzle(time));
    }
    IEnumerator ReloadPuzzle(int time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("test3");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
