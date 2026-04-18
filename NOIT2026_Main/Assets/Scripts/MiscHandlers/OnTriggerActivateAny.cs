using UnityEngine;

public class OnTriggerActivateAny : MonoBehaviour
{
    [SerializeField] GameObject gameObjectToActivate;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObjectToActivate.SetActive(true);
            Destroy(gameObject);
        }
    }
}
