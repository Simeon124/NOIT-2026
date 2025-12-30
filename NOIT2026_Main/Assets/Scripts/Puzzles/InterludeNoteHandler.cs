using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterludeNoteHandler : MonoBehaviour
{
    GlobalIngameTimeHandler globalIngameTimeHandler;

    [SerializeField] private GameObject notePanel;
    [SerializeField] private List<GameObject> notes; 

    [SerializeField] private GameObject noteUIElement;
    [SerializeField] private string text;
    
    bool notesJoined = false;
    
    TextMeshProUGUI noteUIText;
    
    void Start()
    {
        globalIngameTimeHandler = GameObject.FindAnyObjectByType<GlobalIngameTimeHandler>();
    }
    
    void Update()
    {
        if (notes.TrueForAll(x => !x.activeSelf) && notesJoined == false)
        {
            notePanel.SetActive(false);
            globalIngameTimeHandler.gameIsPaused = true;
            
            noteUIElement.GetComponentInChildren<TextMeshProUGUI>().text = text;
            noteUIElement.SetActive(true);
            notesJoined = true;
        }
    }
}