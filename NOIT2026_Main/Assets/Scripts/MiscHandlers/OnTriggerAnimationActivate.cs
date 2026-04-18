using UnityEngine;

public class OnTriggerAnimationActivate : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject backgMusic;
    [SerializeField] private GameObject targetGO;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetGO.SetActive(true);
            player.SetActive(false);
            if (backgMusic != null)
            {
                backgMusic.SetActive(false);
            }
        }
    }
}
