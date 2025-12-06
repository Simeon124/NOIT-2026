using UnityEngine;
using UnityEngine.SceneManagement;

public class FInishedTransitionHandler : MonoBehaviour
{
    //Minimum correct answers to trigger transition
    [SerializeField] private int minimumCorAnswers;
    private int correctAnswersCounter;
    
    //Index of the scene that the player will be transitioned to
    [SerializeField] private int sceneIndex;
    
    void Update()
    {
        if (correctAnswersCounter >= minimumCorAnswers)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
    
    public void IncrementCounter()
    {
        correctAnswersCounter++;
    }
}
