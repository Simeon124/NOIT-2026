using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorial : MonoBehaviour
{
    [SerializeField] List<KeyCode> tutorialKeys = new List<KeyCode>();

    void Update()
    {
        foreach (KeyCode key in tutorialKeys)
        {
            if(Input.GetKeyDown(key))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
