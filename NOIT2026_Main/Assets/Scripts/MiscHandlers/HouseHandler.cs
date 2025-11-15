using UnityEngine;

public class HouseHandler : MonoBehaviour
{
    [SerializeField] EnemyNavigation enemyNavigation;
    [SerializeField] Sanity playerSanity;
    [SerializeField] float enemySanityValueSpawn;
    bool hasSpawned = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Update()
    {
        if (playerSanity.currentSanity <= enemySanityValueSpawn && hasSpawned == false)
        {
            enemyNavigation.canSpawn = true;
            hasSpawned = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyNavigation.playerIsInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyNavigation.playerIsInZone = false;
            enemyNavigation.canSpawn = false;
            hasSpawned = false;
        }
    }
}
