using UnityEngine;

public class FragmentTutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorialUIGameObject;

    private void OnTriggerEnter(Collider other)
    {
        tutorialUIGameObject.SetActive(true);
    }

}
