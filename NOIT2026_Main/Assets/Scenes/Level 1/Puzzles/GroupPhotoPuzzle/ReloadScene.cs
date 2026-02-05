using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
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
