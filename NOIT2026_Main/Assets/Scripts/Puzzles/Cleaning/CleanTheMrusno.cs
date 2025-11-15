using System;
using UnityEngine;

public class CleanTheMrusno : MonoBehaviour
{
    public GameObject mother;
    [SerializeField] int minCleanCount = 300;
    [SerializeField] private GameObject clothesPuzzle;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MRUSOTIQ")
        {
            Destroy(other.gameObject);
        }
    }
}