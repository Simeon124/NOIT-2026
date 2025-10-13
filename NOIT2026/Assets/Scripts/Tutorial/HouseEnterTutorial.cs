using UnityEngine;

public class HouseEnterTutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorialUIGameObject;

    private void OnTriggerEnter(Collider other)
    {
        tutorialUIGameObject.SetActive(false);
    }
}
