using System;
using UnityEngine;

public class InGameUIHandler : MonoBehaviour
{
    GlobalIngameTimeHandler globalIngameTimeHandler;

    private void Start()
    {
        globalIngameTimeHandler = GameObject.FindAnyObjectByType<GlobalIngameTimeHandler>();
    }

    public void CloseMainPanel(GameObject panel)
    {
        globalIngameTimeHandler.gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        panel.SetActive(false);
    }
}