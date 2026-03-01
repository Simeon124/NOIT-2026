using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterludeNoteHandler : MonoBehaviour
{
    GlobalIngameTimeHandler globalIngameTimeHandler;
    
    [SerializeField] private GameObject[] notePanels;
    
    [SerializeField] private GameObject notePanel;
    [SerializeField] private List<GameObject> notes; 

    [SerializeField] private GameObject noteUIElement;
    [SerializeField] private string text;

    [SerializeField] private GameObject basementGO;
    [SerializeField] private GameObject terrainGO;
    
    bool notesJoined = false;
    
    void Start()
    {
        globalIngameTimeHandler = GameObject.FindAnyObjectByType<GlobalIngameTimeHandler>();
    }
    
    void Update()
    {
        if (notes.TrueForAll(x => !x.activeSelf) && notesJoined == false)
        {
            if (notePanel != null)
            {
                notePanel.SetActive(false); 
            }
            else
            {
                foreach (var notePanel in notePanels)
                {
                    notePanel.SetActive(false);
                }
            }
            
            globalIngameTimeHandler.gameIsPaused = true;
            
            noteUIElement.SetActive(true);
            notesJoined = true;
            basementGO.SetActive(true);
            terrainGO.SetActive(false);
        }
    }
}