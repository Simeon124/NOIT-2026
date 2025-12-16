using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject flashlight;
    [SerializeField] bool isOn = false;
    [SerializeField] KeyCode flashlightKey;
    
    void Update()
    {
        if (Input.GetKeyDown(flashlightKey))
        {
            isOn = !isOn;
        }

        FlashlightControl();

    }

    void FlashlightControl()
    {
        if (isOn)
        {
            flashlight.SetActive(true);
        }
        else
        {
            flashlight.SetActive(false);
        }
    }
}
