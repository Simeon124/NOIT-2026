using UnityEngine;

public class EnemySightDetection : MonoBehaviour
{
    [SerializeField] Transform detectionPos;
    [SerializeField] Transform returnPos;
    Sanity playerSanitySettings;

    private void Start()
    {
        playerSanitySettings = GetComponent<Sanity>();
    }
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(detectionPos.transform.position, detectionPos.transform.forward, out hit, 50f))
        {
            if (hit.transform.tag == "Enemy")
            {
                PlayerStatReseter();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(detectionPos.transform.position, detectionPos.transform.forward);
    }

    void PlayerStatReseter()
    {
        transform.position = returnPos.position;

        playerSanitySettings.currentSanity = playerSanitySettings.maxSanity;
    }
}
