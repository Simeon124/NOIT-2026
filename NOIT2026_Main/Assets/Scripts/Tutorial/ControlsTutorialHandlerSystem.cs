using TMPro;
using UnityEngine;

public class ControlsTutorialHandlerSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] private KeyCode controlKey;

    // Update is called once per frame
    void Update()
    {
        if (tutorialText.gameObject.activeSelf && Input.GetKey(controlKey))
        {
            tutorialText.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tutorialText.gameObject.SetActive(true);
        }
    }
}
