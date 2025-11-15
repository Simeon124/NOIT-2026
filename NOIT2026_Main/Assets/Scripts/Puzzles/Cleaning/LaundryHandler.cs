using System;
using UnityEngine;

public class LaundryHandler : MonoBehaviour
{
    [SerializeField] AnimationSceneChangeManager _animationSceneChangeManager;
    [SerializeField] private int cutsceneIndex = 6;

    [SerializeField] private GameObject laundryParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (laundryParent.transform.childCount <= 0)
        {
            _animationSceneChangeManager.ChangeToSceneIndex(cutsceneIndex);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Clothing")
        {
            Destroy(other.gameObject);
        }
    }
}
