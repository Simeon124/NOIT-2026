using System;
using TMPro;
using UnityEngine;

public class ControlsTutorialHandlerSystem : MonoBehaviour
{
    [SerializeField] GameObject targetUIElement;
    [SerializeField] private KeyCode controlKey;
    GlobalIngameTimeHandler globalIngameTimeHandler;

    private void Start()
    {
        globalIngameTimeHandler = GameObject.FindAnyObjectByType<GlobalIngameTimeHandler>();
    }

    void Update()
    {
        if (targetUIElement.gameObject.activeSelf && Input.GetKeyUp(controlKey))
        {
            globalIngameTimeHandler.gameIsPaused = false;
            targetUIElement.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            globalIngameTimeHandler.gameIsPaused = true;
            targetUIElement.gameObject.SetActive(true);
        }
    }
}
