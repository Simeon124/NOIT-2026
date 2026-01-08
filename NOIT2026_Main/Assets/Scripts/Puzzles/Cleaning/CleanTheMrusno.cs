using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;
using Random = UnityEngine.Random;

public class CleanTheMrusno : MonoBehaviour
{
    public GameObject mother;
    [SerializeField] int minCleanCount = 300;
    [SerializeField] private GameObject clothesPuzzle;
    private GameObject player;
    [SerializeField] private int trashQuantity;
    [SerializeField] List<GameObject> trashPrefabs;
    [SerializeField] List<Collider>  boundaries;

    [SerializeField] private bool isHead;

    [Header("UI")] 
    [SerializeField] Slider trashUIDisplay;
    int collectedTrashQuantity = 0;

    private void Start()
    {
        if (!isHead)
        {
            for (int i = 0; i < trashQuantity; i++)
            {
                var choosenBoundary = boundaries[Random.Range(0, boundaries.Count)];
                var randomTrash =  trashPrefabs[Random.Range(0, trashPrefabs.Count)];
                var randomPos = new Vector3(Random.Range(choosenBoundary.bounds.min.x, choosenBoundary.bounds.max.x), 0.4f, Random.Range(choosenBoundary.bounds.min.z, choosenBoundary.bounds.max.z));
                var instantiatedObj = Instantiate(randomTrash, randomPos, randomTrash.transform.rotation);
                instantiatedObj.transform.parent = transform;
            }
        
            player = GameObject.FindWithTag("Player");
        }
        
    }

    void Update()
    {
        if (isHead)
        {
            trashUIDisplay.value = collectedTrashQuantity;
        }
        
        
        if (!isHead)
        {
            if (mother != null)
            {
                if (mother.transform.childCount < minCleanCount) //kogato puzela e minat
                {
                    if (player != null)
                    {
                        player.gameObject.SetActive(false);
                    }

                    Cursor.lockState = CursorLockMode.None;
                    clothesPuzzle.SetActive(true);
                }   
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MRUSOTIQ")
        {
            collectedTrashQuantity++;
            Destroy(other.gameObject);
        }
    }
}