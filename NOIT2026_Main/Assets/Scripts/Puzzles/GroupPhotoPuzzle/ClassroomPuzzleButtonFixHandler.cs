using UnityEngine;

public class ClassroomPuzzleButtonFixHandler : MonoBehaviour
{
    public static bool inConversation = true;

    [SerializeField]
    RandomPosition randomPosition;

    public void TurnOnPuzzle()
    {
        StartCoroutine(randomPosition.IntroFix(0));
    }
}
