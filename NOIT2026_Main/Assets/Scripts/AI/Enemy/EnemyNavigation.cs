using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public bool playerIsInZone = false;
    public bool canSpawn = false;
    bool isSpawned = false;

    [SerializeField] List<Transform> positons = new List<Transform>();
    [SerializeField] GameObject enemy;
    NavMeshAgent enemyNavMeshAgent;

    void Update()
    {
        if (playerIsInZone == true)
        {
            Spawn();

            if (isSpawned == true)
            {
                Move();
            }
        }
        else
        {
            enemy.SetActive(false);
            isSpawned = false;
        }

        
    }

    private void Move()
    {
        enemyNavMeshAgent = enemy.GetComponent<NavMeshAgent>();

        enemyNavMeshAgent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void Spawn()
    {
        if (canSpawn == true)
        {
            enemy.transform.position = positons[UnityEngine.Random.Range(0, positons.Count)].position;
            enemy.SetActive(true);
            canSpawn = false;
            isSpawned = true;
        }
    }
}
