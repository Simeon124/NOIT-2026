using System;
using UnityEngine;

public class InstructionsCounter : MonoBehaviour
{
    private int counter;
    [SerializeField]  private GameObject instructions;
    
    private void OnEnable()
    {
        counter++;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > 1)
        {
            instructions.SetActive(false);
        }
    }
}
