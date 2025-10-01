using UnityEngine;
using UnityEngine.UI;

public class ButtonPick : MonoBehaviour
{
    public Button button1, button2,button3;
    public int correct = 0;
    public bool greenbtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LockButtons()
    {
        button3.interactable = false; 
        button1.interactable = false;
        button2.interactable = false;
        if (greenbtn) { 
            correct++; 
            //play correct animation and change buttons
        }
        else{
            //play wrong animation and change buttons
        }
    }
}
